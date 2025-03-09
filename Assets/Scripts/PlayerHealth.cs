using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Text ScoreText; // Reference to the UI Text element for displaying the score

    public Slider healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        // Initialize the score text (optional)
        if (ScoreText != null)
        {
            int score = PlayerPrefs.GetInt("score", 0);
            ScoreText.text = "SCORE: " + score.ToString();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reset time scale and pause state before loading the new scene
        Time.timeScale = 1f; // Ensure the game is unpaused

        // Retrieve the current score from PlayerPrefs
        int score = PlayerPrefs.GetInt("score", 0);

        // Deduct 150 points from the score
        score -= 150;

        // Ensure the score doesn't go below 0 (optional)
        if (score < 0)
        {
            score = 0;
        }

        // Save the updated score back to PlayerPrefs
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();

        // Update the score text (if ScoreText is assigned)
        if (ScoreText != null)
        {
            ScoreText.text = "SCORE: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText reference is null! Assign the ScoreText UI element in the Inspector.");
        }

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}