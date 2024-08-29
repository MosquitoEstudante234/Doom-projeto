using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Stats do Player " +
        "")]

    [Range(0,100)]
    [SerializeField] int maxLife;
    public int currentLife;

    [Range(0, 100)]
    [SerializeField]int maxShield;
    public int currentShield;

    [Range(0, 999)]
    [SerializeField] int maxAmmo;
     public int currentAmmo;


    [Header("Elementos do canva" +
        "")]
    [SerializeField] Slider lifeInfo;
    [SerializeField] Slider shieldInfo;
    [SerializeField] TMP_Text ammoInfo;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject lightUp;

    [Header("Indicador de Dano")]
    public ParticleSystem bloodExplosion;
    public AudioSource painGrunt;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        lifeInfo.maxValue = maxLife;
        shieldInfo.maxValue = maxShield;

        currentLife = maxLife;
    }
    private void Update()
    {
        if(currentLife > maxLife) 
        {
            currentLife = maxLife;
        }

        if (currentShield > maxShield)
        {
            currentShield = maxShield;
        }

        lifeInfo.value = currentLife;
        shieldInfo.value = currentShield;
        ammoInfo.text = currentAmmo.ToString();

    }

    public void ScreenLight() 
    {
        StartCoroutine(LightUp());
    }
    IEnumerator LightUp() 
    { 
        lightUp.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        lightUp.SetActive(false);

    }

    public void takeDamage(int damage) 
    { 
        
        if (currentShield > 0) 
        { 
            currentShield -= damage;

        }
        else
            currentLife -= damage;


        if(currentLife <= 0) 
        { 
            GetComponent<PlayerMovement>().isDead = true;
            GetComponentInChildren<Gun>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            deathPanel.SetActive(true);
        }
        else 
        {
            if (painGrunt != null) 
            {
                painGrunt.Play();
            }
        }
    }

}
