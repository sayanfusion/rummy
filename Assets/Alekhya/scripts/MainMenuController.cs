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
    [SerializeField] private GameObject GamseStatsOptions;


    private void Awake()
    {
        mainMenumodel = new MainMenuModel();
        mainMenuView.OnPlayPressed += HandlePlayButton;
        mainMenuView.OnSettingsPressed += HandleSettingButton;
        mainMenuView.OnQuitPressed += HandleQuitButton;
        mainMenuView.OnBackPressed += HandleBackButton;
        mainMenuView.OnGameStatsPressed += HandleGameStatsButton;

        // SettingsOptions.SetActive(false);
    }




    private void HandlePlayButton()
    {
        Debug.Log("I am clicked");
        SceneManager.LoadScene("RummyGame");
    }


    public void HandleBackButton()
    {
        Debug.Log("I am back to the mainmenu");
        SceneManager.LoadScene("PlayerMainMenu");
        SettingsOptions.SetActive(false);
    }

    public void HandleSettingButton()
    {
        mainMenumodel.OnOpenSettings();
        SettingsOptions.SetActive(true);
    }


    // private void HandleSettingButtonClose()
    // {
    //     mainMenumodel.OnCloseSettings();
    // }


    private void HandleQuitButton()
    {
        Application.Quit();
    }


    private void HandleGameStatsButton()
    {
        GamseStatsOptions.SetActive(true);
    }

    
}
