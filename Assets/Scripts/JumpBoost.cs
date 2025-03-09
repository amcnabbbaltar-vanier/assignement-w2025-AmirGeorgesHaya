using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPowerUp : MonoBehaviour
{
    private float rotateSpeed = 50f;
    private Vector3 startPos;
    private MeshRenderer mesh;
    private Collider collider;


    void Start(){
        startPos = transform.position;
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
        float newY = Mathf.Sin(Time.time * 5) * 0.25f;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player){
        CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        ParticleSystem playerParticles = player.GetComponentInChildren<ParticleSystem>();
  playerParticles.Play();
        Debug.Log("Power up has been picked up.");

        player.GetComponent<CharacterMovement>().CanFlip = true;

        mesh.enabled = false;
        collider.enabled = false;

        yield return new WaitForSeconds(30.0f);

        Debug.Log("Flip off!");
        player.GetComponent<CharacterMovement>().CanFlip = false;
    }
}