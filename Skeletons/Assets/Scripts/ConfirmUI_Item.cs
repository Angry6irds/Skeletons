using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Alejandro
{
    public class ConfirmUI_Item:  UIWindow
    {
        [Header("Item Confirm References")]
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI itemStatsText;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image itemConfirmIcon;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button returnButton;

        private ItemData _currentItem;

        public override void Initialize()
        {
            base.Initialize();
            if (buyButton != null) buyButton.onClick.AddListener(OnBuyClicked);
            if (cancelButton != null) cancelButton.onClick.AddListener(OnCancelClicked);
            returnButton.onClick.AddListener(OnReturnClick);
        }

        public override void Show()
        {
            canvas.gameObject.SetActive(true);
            rectTransform.localScale = Vector3.one;
            canvasGroup.alpha = 0f;
            
            canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.OutQuad);
        }

        public override void Hide()
        {
            canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.InQuad).OnComplete(() => canvas.gameObject.SetActive(false));
        }

        private void OnReturnClick()
        {
            Hide();
            UiManager.Instance.ShowWindow(WindowsId.ShopUI);
        }

        public void SetupConfirmation(ItemData data)
        {
            _currentItem = data;
            
            if (itemNameText != null) itemNameText.text = data.ItemName;
            if (itemStatsText != null) itemStatsText.text = data.StatsDescription;
            if (itemIcon != null) itemIcon.sprite = data.Icon;
            if (itemConfirmIcon != null) itemConfirmIcon.sprite = data.Icon;
        }

        private void OnBuyClicked()
        {
            if (_currentItem == null) return;

            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(_currentItem);
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
}