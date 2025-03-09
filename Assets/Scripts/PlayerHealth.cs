using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI ScoreText; // Reference to the TextMeshProUGUI component

    private void Start()
    {
        // Load the current health from PlayerPrefs
        currentHealth = PlayerPrefs.GetInt("PlayerHealth", maxHealth);
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        // Update the score text when the scene starts
        UpdateScoreText();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        // Save the current health to PlayerPrefs
        PlayerPrefs.SetInt("PlayerHealth", currentHealth);
        PlayerPrefs.Save();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reset health to max when the player dies
        PlayerPrefs.SetInt("PlayerHealth", maxHealth);
        PlayerPrefs.Save();

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

        // Update the score text in the UI
        UpdateScoreText();

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void UpdateScoreText()
    {
        // Retrieve the current score from PlayerPrefs
        int score = PlayerPrefs.GetInt("score", 0);

        // Update the score text in the UI (if ScoreText is assigned)
        if (ScoreText != null)
        {
            ScoreText.text = "SCORE: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("ScoreText reference is null! Assign the ScoreText UI element in the Inspector.");
        }
    }
}