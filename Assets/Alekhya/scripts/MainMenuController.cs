// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class MainMenuController : MonoBehaviour
// {
//     public MainMenuView mainMenuView;
//     // private MainMenuModel mainMenumodel;
//     [SerializeField] private GameObject SettingsOptions;

//     private void Awake()
//     {
//         // mainMenumodel = new MainMenuModel();
//         mainMenuView.OnPlayPressed += HandlePlayButton;
//         mainMenuView.OnSettingsPressed += HandleSettingButton;
//         mainMenuView.OnQuitPressed += HandleQuitButton;
//     }
//     private void HandlePlayButton()
//     {
//         Debug.Log("I am clicked");
//         string sceneName = "gameScene";

//         if (Application.CanStreamedLevelBeLoaded(sceneName))
//         {
//             Debug.Log("I have Entered The Rummy Game");
//             SceneManager.LoadScene(sceneName);
//         }
//         else
//         {
//             Debug.Log("SceneName is no Valid");
//         }
//     }

//     public void HandleSettingButton()
//     {
//         SettingsOptions.SetActive(true);
//     }

//     public void HandleBackButton()
//     {
//         SettingsOptions.SetActive(false);
//     }


//     private void HandleQuitButton()
//     {
//         Application.Quit();
//     }
// }























using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private GameObject currActivePanel;
    [SerializeField] private List<MainMenuPanelData> mainMenuPanelDataList;
    void Start()
    {

        foreach (var item in mainMenuPanelDataList)
        {
            item.panelOpenButton.onClick.AddListener(() => OpenPanel(item.panelObject));
            item.panelCloseButton.onClick.AddListener(() => ClosePanel());
        }
    }

    void OpenPanel(GameObject panel)
    {
        if (currActivePanel != null) ClosePanel();
        if (!menuPanel.activeSelf) menuPanel.SetActive(true);
        panel.SetActive(true);
        currActivePanel = panel;
    }
    void ClosePanel()
    {
        if (currActivePanel != null)
        {
            currActivePanel.SetActive(false);
            currActivePanel = null;
        }

    }
    
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
}


[System.Serializable]
public class MainMenuPanelData
{
    public Button panelOpenButton;
    public Button panelCloseButton;
    public GameObject panelObject;
}
