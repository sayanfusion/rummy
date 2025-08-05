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
    [SerializeField] private GameObject[] allPanels;
    [SerializeField] private GameObject defaultPanel;


    private void Awake()
    {
        mainMenumodel = new MainMenuModel();
        mainMenuView.OnPlayPressed += HandlePlayButton;
        mainMenuView.OnSettingsPressed += HandleSettingButton;
        mainMenuView.OnQuitPressed += HandleQuitButton;
        mainMenuView.OnBackPressed += HandleBackButton;
        mainMenuView.OnGameStatsPressed += HandleGameStatsButton;
    }

    private void Start()
    {
        
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


    public void HandleBackButton()
    {
        Debug.Log("I am back to the mainmenu");
        SceneManager.LoadScene("PlayerMainMenu");
    }

    public void HandleSettingButton()
    {
        mainMenumodel.OnOpenSettings();
    }


    private void HandleQuitButton()
    {
        Application.Quit();
    }


    private void HandleGameStatsButton()
    {
        GamseStatsOptions.SetActive(true);
        defaultPanel.SetActive(true);
    }

    private void ShowOnePanel(GameObject panelShow)
    {
        foreach(GameObject panel in allPanels)
        {
            panel.SetActive(panel == panelShow);
        }
    }


    public void ShowGameInfoPanel()
    {
        ShowOnePanel(allPanels[0]);
    }
    public void ShowDiscardPilePanel()
    {
        ShowOnePanel(allPanels[1]);
    }
    public void ShowLastGamePanel()
    {
        ShowOnePanel(allPanels[2]);
    }
    public void ShowGameRulesPanel()
    {
        ShowOnePanel(allPanels[3]);
    }
    public void ShowDeckPanel()
    {
        ShowOnePanel(allPanels[4]);
    }
    

    
}
