using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
 public void PlayGame(){

SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);


 }

 public void QuitGame(){
    Debug.Log("QUIT");
    Application.Quit();
 }

 
 public void Restart(){
   // Reset the timer before loading the scene
        if (Timer.Instance != null)
        {
            Timer.Instance.ResetTimer();
            Timer.Instance.ResumeTimer();
        }

        // Load the first level (assuming Level 1 is at build index 1)
        SceneManager.LoadScene(1);
    

 }
}
