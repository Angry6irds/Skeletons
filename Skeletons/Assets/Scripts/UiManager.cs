using System.Collections.Generic;
using NaughtyAttributes;
using NUnit.Framework.Constraints;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance
    {
        get; private set;
    }
    [SerializeField] private List<UIWindow> windows = new List<UIWindow>();

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
    }



    
    
    
    [Button]
    private void FoundUIScene()
    {
        windows.Clear();
        var uiWindows = gameObject.GetComponentsInChildren<UIWindow>(true);
        foreach (var uiWindow in uiWindows)
        {
            if (!windows.Contains(uiWindow))
            {
                windows.Add(uiWindow);
            }
        }
    }

    public void ShowWindow(string windowId)
    {
        UIWindow windowToShow = null;
        foreach (UIWindow window in windows)
        {
            if (window.WindowId == windowId)
            {
                windowToShow = window;
                break;
            }
        }

        if (windowToShow != null)
        {
            windowToShow.Show();
        }
        else
        {
            Debug.LogError($"No se encontro la ventana con ID {windowId}");
        }

    }

    public void HideWindow(string windowId)
    {
        UIWindow windowToHide = null;
        foreach (UIWindow window in windows)
        {
            if (window.WindowId == windowId)
            {
                windowToHide = window;
                break;
            }
        }

        if (windowToHide != null)
        {
            windowToHide.Hide();
        }
        else
        {
            Debug.LogError($"No se encontro la ventana {windowId}");
        }
        
    }


    void Update()
    {
        
    }

    public UIWindow GetWindow(string windowId)
    {
        foreach (var ui in windows)
        {
            if (ui.WindowId == windowId)
            {
                return ui;
            }
        }
        Debug.LogError("No window found with id =" + windowId);
        return null;
    }
}


public static class WindowsId
{
    public const string PopUpUI = "PopUpUI";
    public const string SettingsUI = "SettingsUI";
    public const string ShopUI = "Shop";
    
}