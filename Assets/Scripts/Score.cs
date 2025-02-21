using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    private float delay = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Pickup(other);
        }
    }     
    void Pickup(Collider player){
        Debug.Log("Power up has been picked up.");

        int score = PlayerPrefs.GetInt("score", 0);

        score+=50;

        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save(); 
        ScoreText.text = "SCORE: " + score.ToString();

        gameObject.active = false;
    }
  
}
