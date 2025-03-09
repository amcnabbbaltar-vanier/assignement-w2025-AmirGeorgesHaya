using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Score score; // Reference to the Score script

    void Start()
    {
        // Debug the score reference
        if (score == null)
        {
            Debug.LogError("Score reference is not assigned in the Inspector!");
        }
        else
        {
            Debug.Log("Score reference is assigned correctly.");
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
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        GameIsPaused = true;
    }

    public void Restart()
    {
        // Check if the score reference is null
        if (score == null)
        {
            Debug.LogError("Score reference is null! Assign the Score component in the Inspector.");
            return;
        }

        // Reset time scale and pause state before loading the new scene
        Time.timeScale = 1f; // Ensure the game is unpaused
        GameIsPaused = false;

        // Call the Reset method from the Score script
        score.Reset();

        // Load the new scene
        SceneManager.LoadScene("Level1");
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