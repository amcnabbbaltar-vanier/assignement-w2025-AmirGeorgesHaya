using System.Collections;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    private float delay = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        // Update logic can remain if needed
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Pickup(collider);
        }
    }

    void Pickup(Collider player)
    {
        // Get the CharacterMovement component
        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();

        if (characterMovement != null)
        {
            // Double the speed multiplier
            characterMovement.speedMultiplier *= 2;

            // Start the coroutine to reset the speed multiplier after the delay
            StartCoroutine(ResetSpeedMultiplier(characterMovement));

            // Disable the buff object
            gameObject.SetActive(false);
        }
        else
        {
            // Log an error if CharacterMovement is not found
            Debug.LogError("CharacterMovement component not found on player.");
        }
    }

    // Coroutine to reset speed multiplier after a delay
    IEnumerator ResetSpeedMultiplier(CharacterMovement characterMovement)
    {
        // Wait for the specified delay (5 seconds)
        yield return new WaitForSeconds(delay);

        // Reset the speed multiplier back to normal (half of what it was)
        characterMovement.speedMultiplier /= 2;

        // Optional: Log message to confirm the reset
        Debug.Log("Speed multiplier reset.");
    }
}
