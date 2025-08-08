using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void HandlePlayButton()
    {
        Debug.Log("I am clicked");
        string sceneName = "gameScene";

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log("I have Entered The Rummy Game");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("SceneName is no Valid");
        }
    }

    public void HandleExitButton()
    {
        Debug.Log("I am clicked");
        string sceneName = "mainMenu";

        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.Log("returned to the lobby");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("SceneName is no Valid");
        }
    } 


      public void HandleQuitButton()
    {
        Debug.Log("I Quit");
        Application.Quit();
    }   
}
