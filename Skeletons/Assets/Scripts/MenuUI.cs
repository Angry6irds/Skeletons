using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public override void Show()
    {
        canvas.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        rectTransform.localScale = Vector3.one * 0.9f;
        
        canvasGroup.DOFade(1f, 0.5f);
        rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutCubic);
    }

    public override void Hide()
    {
        canvasGroup.DOFade(0f, 0.4f);
        rectTransform.DOScale(Vector3.one * 0.9f, 0.4f).SetEase(Ease.InCubic).OnComplete(() => canvas.gameObject.SetActive(false));
    }

    private void OnPlayClicked()
    {
        UiManager.Instance.HideWindow(WindowsId.MenuUI);
        UiManager.Instance.ShowWindow(WindowsId.GameplayUI);
        if (Alejandro.Gameplay.GameManager.Instance != null)
        {
            Alejandro.Gameplay.GameManager.Instance.StartGame();
        }
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
