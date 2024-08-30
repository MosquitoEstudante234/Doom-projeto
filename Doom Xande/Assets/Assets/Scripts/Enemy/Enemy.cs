using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Efeitos de dano")]
    public ParticleSystem blood;
    public Outline outline;

    Animator anim;
    bool haveAnim;

    [Header("Ataque do inimigo")]
    public float firstAttack;
    public float attackTimer;
    float timeToAttack;
    [Header("O que ele faz quando ve o player")]
    public UnityEvent onSeePlayer;

    [Header("Layer pro Raycast/ botar a layer default")]
    public LayerMask layerMask;

    bool isDead;

    [Header("Vida e Range")]
    public int life = 5;
    public float range = 15;

    Transform playerPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            haveAnim = false;
        }
        else
            haveAnim = true;

        if(outline != null) 
        {
            outline.enabled = false;
        }
        playerPos = PlayerStats.Instance.gameObject.transform;
    }
    private void Update()
    {
        if(life <= 0 && isDead)
        {
            playerPos = null;
        }

        if (playerPos == null)
            return;

        Vector3 playerDirection = playerPos.position - transform.position;

      
        if (Physics.Raycast(transform.position, playerDirection, out RaycastHit hit, range, layerMask))
        {
            if (hit.transform == playerPos)
            {
                Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

                timeToAttack -= Time.deltaTime;
                if (timeToAttack <= 0f && haveAnim)
                {
                    //onSeePlayer.Invoke();
                    anim.SetTrigger("Shoot");
                    timeToAttack = attackTimer;
                }
                else if (timeToAttack <= 0f)
                {
                   onSeePlayer.Invoke();
                   timeToAttack = attackTimer;

                }
            }
        }
        else
        {
            timeToAttack = firstAttack;
        }
    }

    public void OnSeePlayer()
    {
        onSeePlayer.Invoke();

    }
    public void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0 && !isDead)
        {
            isDead = true;
            blood.Play();
            StartCoroutine(DeathEffects());
        }
        else if (life > 0)
        {
            if (!blood.isPlaying)
            {
                blood.Play();
            }
        }
    }

    public void LightUp(bool state) 
    {
        if (outline != null)
        {
            outline.enabled = state;
        }
    }

    public void PlaySound(AudioSource source) 
    { 
        source.Play();
    }

    private IEnumerator DeathEffects()
    {
        yield return new WaitForSeconds(blood.main.duration + 0.5f);

        Destroy(gameObject);
        WinCondition.score--;
    }
}
