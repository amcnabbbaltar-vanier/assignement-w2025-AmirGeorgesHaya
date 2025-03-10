using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    private float rotateSpeed = 50f;
    private Vector3 startPos;
    private MeshRenderer mesh;
    private Collider collider;

    private float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
        float newY = Mathf.Sin(Time.time * 5) * 0.25f;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);  
    }
   void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Pickup(other);
        }
    }     
    void Pickup(Collider player){
         CharacterMovement characterMovement = player.GetComponent<CharacterMovement>();
        ParticleSystem playerParticles = player.GetComponentInChildren<ParticleSystem>();
  playerParticles.Play();
        Debug.Log("Power up has been picked up.");

        int score = PlayerPrefs.GetInt("score", 0);

        score+=50;

        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save(); 
        ScoreText.text = "SCORE: " + score.ToString();

        gameObject.SetActive(false);
    }
    public void Reset()
    {
         int score = PlayerPrefs.GetInt("score", 0);

        score=0;

        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save(); 
        ScoreText.text = "SCORE: " + score.ToString();
       
    }
}
