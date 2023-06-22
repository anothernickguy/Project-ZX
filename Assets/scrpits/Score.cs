using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al objeto TextMeshPro que muestra el puntaje
    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        currentScore += 10;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
