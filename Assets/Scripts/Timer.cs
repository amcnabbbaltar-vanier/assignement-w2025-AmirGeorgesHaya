using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance; // Singleton instance

    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component for displaying the timer
    private float elapsedTime = 0f; // Total elapsed time in seconds
    private bool isPaused = false; // Whether the timer is paused

    private void Awake()
    {
        // Ensure only one instance of Timer exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Reset the timer if this is the first level
        if (SceneManager.GetActiveScene().buildIndex == 1) // Assuming Level 1 is at build index 0
        {
            ResetTimer();
        }
        else
        {
            // Load the elapsed time from PlayerPrefs
            elapsedTime = PlayerPrefs.GetFloat("ElapsedTime", 0f);
        }

        // Find the timerText object in the new scene
        FindTimerText();
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the timerText object in the new scene
        FindTimerText();
    }

    void Update()
    {
        if (!isPaused)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Update the timer text
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        // Convert elapsed time to minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Update the timer text
        if (timerText != null)
        {
            timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    public void PauseTimer()
    {
        isPaused = true;
        UpdateTimerDisplay(); // Ensure the timer text is updated when paused
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        PlayerPrefs.SetFloat("ElapsedTime", elapsedTime); // Reset the stored time
        PlayerPrefs.Save();
        UpdateTimerDisplay();
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void FindTimerText()
    {
        // Find the timerText object in the new scene
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        if (timerText == null)
        {
            Debug.LogError("TimerText reference could not be found in the scene!");
        }
        else
        {
            Debug.Log("TimerText reference found: " + timerText.gameObject.name);
            UpdateTimerDisplay(); // Update the timer text with the current elapsed time
        }
    }
}