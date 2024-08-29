using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Colectable : MonoBehaviour
{
    public UnityEvent OnCollect;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            OnCollect.Invoke();
            Destroy(gameObject);
        }

    }

    public void AddHealth(int healthToAdd) 
    { 
        PlayerStats.Instance.currentLife += healthToAdd;
    }

    public void AddShield(int shieldToAdd) 
    { 
        PlayerStats.Instance.currentShield += shieldToAdd;
    }

    public void AddAmmo(int ammoToAdd) 
    {
        PlayerStats.Instance.currentAmmo += ammoToAdd;
    }
}
