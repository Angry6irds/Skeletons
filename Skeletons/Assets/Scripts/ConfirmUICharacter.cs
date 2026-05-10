using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConfirmUICharacter: UIWindow
{
    [Header("Character Confirm References")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI characterStatsText;
    [SerializeField] private Image characterIcon;
    [SerializeField] private Image characterConfirmIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;

    private ItemData _currentCharacter;

    public override void Initialize()
    {
        base.Initialize();
        if (buyButton != null) buyButton.onClick.AddListener(OnBuyClicked);
        if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClicked);
    }

    public override void Show()
    {
        canvas.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;
        rectTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        
        canvasGroup.DOFade(1f, 0.4f);
        rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
    }

    public override void Hide()
    {
        canvasGroup.DOFade(0f, 0.3f);
        rectTransform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => canvas.gameObject.SetActive(false));
    }

    public void SetupConfirmation(ItemData data)
    {
        _currentCharacter = data;
        
        if (characterNameText != null) characterNameText.text = data.ItemName;
        if (characterStatsText != null) characterStatsText.text = data.StatsDescription;
        if (characterIcon != null) characterIcon.sprite = data.Icon;
        if (characterConfirmIcon != null) characterConfirmIcon.sprite = data.Icon;
    }

    private void OnBuyClicked()
    {
        if (_currentCharacter == null) return;

        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(_currentCharacter);
        }

        UiManager.Instance.HideWindow(WindowId);
        UiManager.Instance.ShowWindow(WindowsId.ShopUI);
    }

    private void OnCancelClicked()
    {
        UiManager.Instance.HideWindow(WindowId);
        UiManager.Instance.ShowWindow(WindowsId.ShopUI);
    }

    private void OnDestroy()
    {
        if (buyButton != null) buyButton.onClick.RemoveListener(OnBuyClicked);
        if (cancelButton != null) cancelButton.onClick.RemoveListener(OnCancelClicked);
    }
}
