using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Certifique-se de importar o namespace TextMeshPro

public class WinCondition : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt; // Use TextMeshProUGUI ao invés de Text
    public static int score;

    private void Start()
    {
        score = 10;
    }

    private void Update()
    {
        scoreTxt.text = "Zombies to Kill: " + score.ToString();
        if (score <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coletavel"))
        {
            score -= 1;
        }
    }
}
