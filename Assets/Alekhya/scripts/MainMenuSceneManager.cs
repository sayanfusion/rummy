using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBackButtonClick()
    {

    }

    public void OnFreePlayClick()
    {

    }  

    public void OnWinPlayClick(int amount)
    {
        SceneManager.LoadScene("RummyGame");
    }

    public void OnHelpClick()
    {

    }
}
