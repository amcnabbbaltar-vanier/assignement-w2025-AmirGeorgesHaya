using UnityEngine;
using TMPro;

public class WinningScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component for displaying the score
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer

    void Start()
    {
        // Load the score from PlayerPrefs
        int score = PlayerPrefs.GetInt("score", 0);

        // Update the score text
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score.ToString();
        }
        else
        {
            Debug.LogError("scoreText reference is null!");
        }

        // Pause the timer and display the total time taken
        if (Timer.Instance != null)
        {
            Timer.Instance.PauseTimer(); // Pause the timer

            // Update the timer text
            if (timerText != null)
            {
                timerText.text = "TIME: " + Timer.Instance.GetFormattedTime();
            }
            else
            {
                Debug.LogError("timerText reference is null!");
            }
        }
        else
        {
            Debug.LogError("Timer instance is null!");
        }
    }
}