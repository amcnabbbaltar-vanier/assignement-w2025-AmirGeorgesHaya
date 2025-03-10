using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour

{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("You've reached the goal");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
           

            StartCoroutine(NextLevel(0.1f)); 
        }
    }

    private IEnumerator NextLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene= currentScene+1;
         if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}