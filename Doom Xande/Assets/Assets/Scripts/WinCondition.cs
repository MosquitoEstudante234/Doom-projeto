using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour
{
    public Text scoreTxt;
    public static int score;
    private void Start()
    {

        score = 10;
    }


    private void Update()
    {
        scoreTxt.text = "Zombies to Kill: " + score.ToString();


    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Coletavel") == true)
        {
            score -= 1;
        }
    }
    }
