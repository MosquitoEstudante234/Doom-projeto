using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Gun : MonoBehaviour
{
    [Header("Tamanho do colider da arma")]
    public float gunRange; 
    public float gunVerticalRange;

    [Header("Taxa de disparo")]
    public float fireRate;
    float timeToFire;

    [Header("Dano")]
    public int damage = 4;

    BoxCollider col;

    int gunLayer;
    int ignoreGunLayerMask;

    [Header("Inimigos no range da arma")]
    public List<Enemy> enemiesOnSight;
    PlayerStats stats;

    [Header("Feedbacks")]
    public ParticleSystem smoke;
    public AudioSource shotSound;
    public AudioSource emptyClip;

    Animator anim;
    private void Awake()
    {
        gunLayer = LayerMask.NameToLayer("gun");
        if(gunLayer == -1) 
        {
            Debug.LogWarning("Criar Layer = gun " +
                "e botar no colider da arma");
            Debug.Break();
        }
        else
        ignoreGunLayerMask = ~(1 << gunLayer);
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        stats = PlayerStats.Instance;

        col = GetComponent<BoxCollider>();
        col.size = new Vector3(1, gunVerticalRange, gunRange);
        col.center = new Vector3(0, 0, gunRange * 0.5f);

    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > timeToFire)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy) 
        {
            enemiesOnSight.Add(enemy);
            enemy.LightUp(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            enemiesOnSight.Remove(enemy);
            enemy.LightUp(false);
        }
    }

    public void Shoot() 
    {
        if (stats.currentAmmo > 0)
        {

            stats.currentAmmo--;
            anim.SetTrigger("Shoot");
            shotSound.Play();

            if (enemiesOnSight.Count > 0)
            {

                enemiesOnSight.RemoveAll(enemy => enemy == null);

                foreach (Enemy enemy in enemiesOnSight) 
                {
                    Vector3 enemyDirection = enemy.transform.position - transform.position;
                    float raycastRange = 100f; 
                    RaycastHit hit;

                    
                    if (Physics.Raycast(transform.position, enemyDirection, out hit, raycastRange, ignoreGunLayerMask))
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.red, 2f);

                        Debug.Log($"Hit {hit.transform.name}"); 
                        if (hit.transform == enemy.transform)
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                }

            }
            else 
            {
                float raycastRange = 100f;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange, ignoreGunLayerMask))
                {
                    smoke.transform.position = hit.point;
                    smoke.Play();
                }
            }
            timeToFire = Time.time + fireRate;
        }
        else
        {
            if (!emptyClip.isPlaying) 
            {
                emptyClip.Play();
            }
            Debug.Log("No Ammo");
        }
            
    }
}
