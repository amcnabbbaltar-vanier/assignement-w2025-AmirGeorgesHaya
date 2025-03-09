using System.Collections;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    private float delay = 5f; // Duration of the speed buff
    private float rotateSpeed = 50f; // Rotation speed of the speed buff
    private Vector3 startPos; // Initial position of the speed buff
    private MeshRenderer mesh; // Reference to the MeshRenderer component
    private Collider collider; // Reference to the Collider component

    // Start is called before the first frame update
    void Start()
    {
        // Initialize components
        startPos = transform.position;
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate and float the speed buff
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        float newY = Mathf.Sin(Time.time * 5) * 0.25f;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player picked up the speed buff
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    // Coroutine to handle the pickup logic
    IEnumerator Pickup(Collider player)
    {
        // Get the CharacterMovement component from the player
        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        if (characterMovement != null)
        {
            // Apply the speed buff
            characterMovement.speedMultiplier *= 2;

            // Get the ParticleSystem component from the player
            ParticleSystem playerParticles = player.GetComponentInChildren<ParticleSystem>();
            if (playerParticles != null)
            {
                // Play the particle effect on the player
                playerParticles.Play();
                Debug.Log("Playing particle effect on the player...");
            }
            else
            {
                Debug.LogWarning("ParticleSystem not found on the player!");
            }

            // Disable the mesh renderer and collider to make the object disappear
            mesh.enabled = false;
            collider.enabled = false;

            // Wait for the delay duration
            yield return new WaitForSeconds(delay);

            // Reset the speed multiplier
            characterMovement.speedMultiplier /= 2;

            // Destroy the object after the delay
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("CharacterMovement component not found on the player!");
        }
    }
}