using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    private float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            Pickup(collider);
        }

    }
    void Pickup(Collider player){
        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        player.GetComponent<CharacterMovement>().speedMultiplier *= 2;
        StartCoroutine(ResetSpeedMultiplier(characterMovement));
        gameObject.SetActive(false);
    }
     IEnumerator ResetSpeedMultiplier(CharacterMovement characterMovement)
    {
        // Wait for the specified delay (5 seconds)
        yield return new WaitForSeconds(delay);

        // Reset the speed multiplier back to normal
        characterMovement.speedMultiplier /= 2;
    }
}
