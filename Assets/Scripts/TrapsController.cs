using UnityEngine;

public class TrapsController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Debug log to check if the collision is detected
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Check if the collider is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched the trap!");

            // Try to get the PlayerHealth component from the player
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
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
        }
    }
}