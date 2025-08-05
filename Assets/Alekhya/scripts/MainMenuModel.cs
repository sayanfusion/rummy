using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel
{
    public bool isSettingOpen { get; set; }
    public bool isgameStatsOpen{ get; set; }
    

    public void OnOpenSettings()
    {
        isSettingOpen = true;

    }
    public void OnOpenGameStatsButton()
    {
        isgameStatsOpen = true;

    }
}
