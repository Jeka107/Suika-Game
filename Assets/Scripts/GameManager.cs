using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        BallCombiner.onCombinedBalls += AddToScore;
    }
    private void OnDestroy()
    {
        BallCombiner.onCombinedBalls -= AddToScore;
    }
    private void AddToScore(int points)
    {
        score += points;
        scoreText.text= score.ToString();
    }
}
