using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmUICharacter: UIWindow
{
    [Header("Character Confirm References")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Image characterIcon;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button cancelButton;

    private ItemData _currentCharacter;

    public override void Initialize()
    {
        base.Initialize();
        if (buyButton != null) buyButton.onClick.AddListener(OnBuyClicked);
        if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClicked);
    }

    public void SetupConfirmation(ItemData data)
    {
        _currentCharacter = data;
        
        if (characterNameText != null) characterNameText.text = data.ItemName;
        if (characterIcon != null) characterIcon.sprite = data.Icon;
        
        Debug.Log($"Preparando compra de PERSONAJE: {data.ItemName}");
    }

    private void OnBuyClicked()
    {
        if (_currentCharacter == null) return;
        
        Debug.Log($"PERSONAJE COMPRADO: {_currentCharacter.ItemName}");

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
