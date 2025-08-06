using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public MainMenuView mainMenuView;
    private MainMenuModel mainMenumodel;
    [SerializeField] private GameObject SettingsOptions;
    [SerializeField] private GameObject GameStatsOptions;
    [SerializeField] private GameStatsOptionsManager gameStatsManager;


    private void Awake()
    {
        mainMenumodel = new MainMenuModel();
        mainMenuView.OnPlayPressed += HandlePlayButton;
        mainMenuView.OnSettingsPressed += HandleSettingButton;
        mainMenuView.OnQuitPressed += HandleQuitButton;
    }
    private void HandlePlayButton()
    {
        Debug.Log("I am clicked");
        string sceneName = "SampleScene";

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

    public void HandleSettingButton()
    {
        SettingsOptions.SetActive(true);
        gameStatsManager.HideGameStatsButton();
    }

    public void HandleBackButton()
    {
        SettingsOptions.SetActive(false);
        gameStatsManager.ShowGameStatsButton();
    }


    private void HandleQuitButton()
    {
        Application.Quit();
    }
}
