using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
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
        player.GetComponent<CharacterMovement>().CanFlip =true;
        gameObject.SetActive(false);
    }
}
