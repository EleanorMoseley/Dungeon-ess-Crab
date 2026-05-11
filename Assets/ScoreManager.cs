using UnityEngine;
using TMPro; // Required namespace for TextMesh Pro

public class ScoreManager : MonoBehaviour
{
    // Reference the TMP component from the Inspector
    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    void Start()
    {
        UpdateScoreDisplay();
        InvokeRepeating("IncrementScore", 1f, 1f);
    }

    void IncrementScore()
    {
        currentScore++;
        scoreText.text = "Score: " + currentScore.ToString();
        UpdateScoreDisplay();
    }

    // Call this method whenever points are earned
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        // Set the text property using string interpolation or concatenation
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
