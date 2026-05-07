using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UIWindow
{
    [Header("Menu Buttons")] [SerializeField]
    private Button playButton;

    [SerializeField] private Button shopButton;
    [SerializeField] private Button settingsButton;

    public override void Initialize()
    {
        base.Initialize();
        playButton.onClick.AddListener(OnPlayClicked);
        shopButton.onClick.AddListener(OnShopClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
    }

    private void OnPlayClicked()
    {
        UiManager.Instance.HideWindow(WindowsId.MenuUI);
        UiManager.Instance.ShowWindow(WindowsId.GameplayUI);
    }

    private void OnShopClicked()
    {
        UiManager.Instance.HideWindow(WindowsId.MenuUI);
        UiManager.Instance.ShowWindow(WindowsId.ShopUI);
    }

    private void OnSettingsClicked()
    {
        UiManager.Instance.HideWindow(WindowsId.MenuUI);
        UiManager.Instance.ShowWindow(WindowsId.SettingsUI);
    }

    private void OnDestroy()
    {
        if (playButton != null)
        {
            playButton.onClick.RemoveListener(OnPlayClicked);
        }

        if (shopButton != null)
        {
            shopButton.onClick.RemoveListener(OnShopClicked);
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
        }
    }
}
