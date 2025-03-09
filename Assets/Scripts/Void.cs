using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Add this line
public class VoidController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell into the void!");

            // Try to get the PlayerHealth component from the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("PlayerHealth component found. Applying damage.");
                // Call the TakeDamage method on the PlayerHealth component
                playerHealth.TakeDamage(1);
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on the player!");
            }

            // Start a coroutine to delay the scene reset
            StartCoroutine(ResetSceneAfterDelay(0.1f)); // Adjust the delay as needed
        }
    }

    private IEnumerator ResetSceneAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}