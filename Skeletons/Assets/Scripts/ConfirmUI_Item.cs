using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Alejandro
{
    public class ConfirmUI_Item:  UIWindow
    {
        [Header("Item Confirm References")]
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button cancelButton;

        private ItemData _currentItem;

        public override void Initialize()
        {
            base.Initialize();
            if (buyButton != null) buyButton.onClick.AddListener(OnBuyClicked);
            if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClicked);
        }

        public void SetupConfirmation(ItemData data)
        {
            _currentItem = data;
            
            if (itemNameText != null) itemNameText.text = data.ItemName;
            if (itemIcon != null) itemIcon.sprite = data.Icon;
            
            Debug.Log($"Preparando compra de ITEM: {data.ItemName}");
        }

        private void OnBuyClicked()
        {
            if (_currentItem == null) return;
            
            Debug.Log($"ITEM COMPRADO: {_currentItem.ItemName}");

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
}