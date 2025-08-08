using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    [SerializeField] private GameObject currActivePanel;
    [SerializeField] private List<PanelData> panelDataList;
    void Start()
    {

        foreach (var item in panelDataList)
        {
            item.panelOpenButton.onClick.AddListener(() => OpenPanel(item.panelObject));
            item.panelCloseButton.onClick.AddListener(() => ClosePanel());
        }
    }

    void OpenPanel(GameObject panel)
    {
        if (currActivePanel != null ) ClosePanel();
        if (!menuPanel.activeSelf) menuPanel.SetActive(true);
        panel.SetActive(true);
        currActivePanel = panel;
    }
    void ClosePanel() { 
        if (currActivePanel != null)
        {
            currActivePanel.SetActive(false);
            currActivePanel = null;
        }

    }

}


[System.Serializable]
public class PanelData
{
    public Button panelOpenButton;
    public Button panelCloseButton;
    public GameObject panelObject;
}