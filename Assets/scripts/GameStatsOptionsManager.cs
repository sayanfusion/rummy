using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsOptionsManager : MonoBehaviour
{
    [SerializeField] private GameObject GamseStatsManager;
    [SerializeField] private Button gameStatsButton;
    [SerializeField] private GameObject GamseStatsOptions;
    [SerializeField] private GameObject[] allPanels;
    [SerializeField] private GameObject defaultPanel;

    private void Awake()
    {
        if (gameStatsButton != null)
        {
            gameStatsButton.onClick.AddListener(HandleGameStatsButton);

        }
        else
        {
            Debug.LogError("GameStatButton is not Assigned");
        }
    }

    private void HandleGameStatsButton()
    {
        Debug.Log("I am GameStats i am clicked");
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

    public void HideGameStatsButton()
    {
        GamseStatsManager.SetActive(false);
    }
    public void ShowGameStatsButton()
    {
        GamseStatsManager.SetActive(true);
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
