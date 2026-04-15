using System;
using UnityEngine;
using UnityEngine.Events;

public class ResponsiveManager : MonoBehaviour
{
    public static ResponsiveManager Instance{get; private set;}

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #region private Fields
    private Vector2 _lastScreenSize;
    #endregion

    #region public Properties
    public ScreenOrientation CurrentOrientation => GetScreenOrientation();


    public DeviceType CurrentDeviceType { get => GetDeviceTypeByResolution(Screen.width, Screen.height); }


    public bool IsPortrait() => Screen.width < Screen.height;
    public bool IsLandscape() => Screen.width >= Screen.height;
    public Vector2 CurrentScreenSize => new Vector2(Screen.width, Screen.height);
    public UnityEvent OnScreenSizeChanged { get; private set; } = new UnityEvent();

    #endregion
    
    #region Unity Methods

    private void OnEnable()
    {
        _lastScreenSize = new Vector2(Screen.width, Screen.height);
        Application.onBeforeRender += CheckScreenSizeChange;
    }

    private void OnDisable()
    {
        Application.onBeforeRender -= CheckScreenSizeChange;
    }

    private void Start()
    {
        Debug.Log(CurrentScreenSize);
        Debug.Log(CurrentOrientation);
        Debug.Log(CurrentDeviceType);
    }
    #endregion
    
    #region private Methods
    private void CheckScreenSizeChange()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (_lastScreenSize != currentScreenSize)
        {
            _lastScreenSize = currentScreenSize;
            OnScreenSizeChanged?.Invoke();
            Debug.Log($" Screen size Change: {currentScreenSize.x}x{currentScreenSize.y} Orientation: {(IsPortrait() ? "Portrait" : "Landscape")}");
            Debug.Log($"Device type: {CurrentDeviceType}");
        }
    }

    private ScreenOrientation GetScreenOrientation()
    {
        return IsPortrait() ? ScreenOrientation.Portrait : ScreenOrientation.Landscape;
    }
    private ScreenOrientation GetScreenOrientation()
    {
        
    }
    private DeviceType GetDeviceTypeByResolution(int width, int height)
    {
        
    }
    #endregion
}
