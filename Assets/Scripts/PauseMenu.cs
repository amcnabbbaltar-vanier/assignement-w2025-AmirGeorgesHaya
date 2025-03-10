using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public PlayerHealth health;
    public Score score; // Reference to the Score script
    public Timer timer; // Reference to the Timer script

    void Start()
    {
        score = FindObjectOfType<Score>();
    if (score == null)
    {
        Debug.LogError("Score reference could not be found in the scene!");
    }
    else
    {
        Debug.Log("Score reference found: " + score.gameObject.name);
    }

    // Find the PlayerHealth component dynamically
    health = FindObjectOfType<PlayerHealth>();
    if (health == null)
    {
        Debug.LogError("PlayerHealth reference could not be found in the scene!");
    }
    else
    {
        Debug.Log("PlayerHealth reference found: " + health.gameObject.name);
    }

    }

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the score reference after the scene loads
        score = FindObjectOfType<Score>();
        if (score == null)
        {
            Debug.LogError("Score reference could not be found after scene load!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unpause the game
        GameIsPaused = false;
          if (Timer.Instance != null)
    {
        Timer.Instance.ResumeTimer();
    }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        GameIsPaused = true;
         if (Timer.Instance != null)
    {
        Timer.Instance.PauseTimer();
    }
    }

   public void Restart()
{
    Debug.Log("Restart method called.");

    // Check if the score reference is null
    if (score == null)
    {
        Debug.LogError("Score reference is null! Assign the Score component in the Inspector.");
        return;
    }
    else
    {
        Debug.Log("Score reference found: " + score.gameObject.name);
    }

    // Check if the health reference is null
    if (health == null)
    {
        Debug.LogError("PlayerHealth reference is null! Assign the PlayerHealth component in the Inspector.");
        return;
    }
    else
    {
        Debug.Log("PlayerHealth reference found: " + health.gameObject.name);
    }

    // Reset the player's health
    health.ResetHealth();

    // Reset the score
    score.Reset();

    // Reset time scale and pause state before loading the new scene
    Time.timeScale = 1f; // Ensure the game is unpaused
    GameIsPaused = false;
if (Timer.Instance != null)
    {
        Timer.Instance.ResumeTimer();
    }
    // Load the new scene
    Scene currentScene = SceneManager.GetActiveScene();
    Debug.Log("Reloading scene: " + currentScene.name);
    SceneManager.LoadScene(currentScene.name);
}
    public void QuitGame()
    {
        // Reset time scale and pause state before loading the menu
        Time.timeScale = 1f; // Ensure the game is unpaused
        GameIsPaused = false;

        // Load the menu scene
        SceneManager.LoadScene("Menu");
    }

}