using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; //light gameObject to work with
    private bool isOn = false; //is flashlight on or off?

    // Use this for initialization
    void Start()
    {
        isOn = true;
        //set default off
        lightGO.SetActive(isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
            if (isOn)
            {
                lightGO.SetActive(true);
            }
            //turn light off

        
    }
}
