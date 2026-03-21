using DG.Tweening;
using NaughtyAttributes;
using NUnit.Framework.Constraints;
using UnityEngine;


public class UIWindow : MonoBehaviour
{
    [Header("UI Window")] [SerializeField] private string windowId;

    [Header("UI References")] [SerializeField]
    public Canvas canvas;

    [SerializeField] public CanvasGroup canvasGroup;

    [SerializeField] public bool hideOnStart = true;

    public string WindowId => windowId;
    
    public RectTransform rectTransform => canvasGroup.GetComponent<RectTransform>();

    private void Awake()
    {

    }

    void Start()
    {
        Initialize();
    }


    public virtual void Initialize()
    {
        canvas.gameObject.SetActive(!hideOnStart);
        rectTransform.localScale = Vector3.zero;
    }

    [Button]
    public virtual void Show()
    {
        canvas.gameObject.SetActive(true);

        Debug.Log($"Showing window {windowId}");
        rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            
        });
    }

    [Button]
    public virtual void Hide()
    {
        Debug.Log($"Hideing window {windowId}");
        rectTransform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            canvas.gameObject.SetActive(false);
        });
    }
}

