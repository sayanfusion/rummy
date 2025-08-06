using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingButton;
    [SerializeField] private Button QuitButton;
    // [SerializeField] private Button BackButton;


    public event Action OnPlayPressed;
    public event Action OnSettingsPressed;
    public event Action OnQuitPressed;
    // public event Action OnBackPressed;
    public event Action OnGameStatsPressed;

    private void Start()
    {
        PlayButton.onClick.AddListener(() => OnPlayPressed?.Invoke());
        SettingButton.onClick.AddListener(() => OnSettingsPressed?.Invoke());
        QuitButton.onClick.AddListener(() => OnQuitPressed?.Invoke());
        // BackButton.onClick.AddListener(() => OnBackPressed?.Invoke());

    }
}
