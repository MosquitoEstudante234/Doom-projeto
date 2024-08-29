using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Light _light;

    float maxIntensity;
    float minIntensity = 1f;

    float changeSpeed = 1.5f;

    float randomNum,timeInterval = 0.2f;
    
    private void Start()
    {
       _light = GetComponent<Light>();
        maxIntensity = _light.intensity;

        randomNum = UnityEngine.Random.Range(0, 100);

        StartCoroutine(UpdateTorchLight());
    }


    IEnumerator UpdateTorchLight()
    {
        while (true)
        {
            float noise = Mathf.PerlinNoise(randomNum, Time.time * changeSpeed);
            //perlin noise é um argumento pseudo-aleatorio q consiste em gerar "noise" num plano 2d,
            //é pseudo aleatorio pq vem em ondas que aumentam e diminuem seu valor, bora bill
            _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

            yield return new WaitForSeconds(timeInterval);
        }
    }

}
