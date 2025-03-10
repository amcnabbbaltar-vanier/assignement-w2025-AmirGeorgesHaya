using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You've reached the goal");

            // Store the current timer value before loading the next level
            if (Timer.Instance != null)
            {
                PlayerPrefs.SetFloat("ElapsedTime", Timer.Instance.GetElapsedTime());
                PlayerPrefs.Save();
            }

            StartCoroutine(NextLevel(0.1f));
        }
    }

    private IEnumerator NextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}