using System.Collections.Generic;
using UnityEngine;
using ScriptableObjet;
using UnityEditor;
using UnityEngine.UI;
namespace Alejandro
{
    public class Shop_UI : UIWindow
    {
        [Header("Shop References")]
        [SerializeField]private GameObject itemPrefab;
        [SerializeField]private Transform itemContentContainer;
        [SerializeField]private Transform characterContentContainer;
        [SerializeField]private Button confirmButton;

        [Header("Data to Load")] [SerializeField]
        private List<ItemData> allAvailableItems;
        
        private ItemData _currentSelectedItem;

        public override void Initialize()
        {
            base.Initialize();
            confirmButton.gameObject.SetActive(false);
            confirmButton.onClick.AddListener(OnConfirmClicked);
            PopulateShop();
        }

        private void PopulateShop()
        {
            foreach (Transform child in itemContentContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in characterContentContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (ItemData item  in allAvailableItems)
            {
                Transform targetContainer = IsCharacter(item.itemType) ? characterContentContainer : itemContentContainer;
                GameObject newItemGo = Instantiate(itemPrefab, targetContainer);
                ItemUI itemUI = newItemGo.GetComponent<ItemUI>();
                if (itemUI != null)
                {
                    itemUI.SetData(item);
                    itemUI.OnItemClicked += HandleItemSelected;
                }
            }
        }

        private bool IsCharacter(ItemType type)
        {
            return type == ItemType.skeleton || type == ItemType.iceSkeleton || type == ItemType.mudSkeleton;
        }

        private void HandleItemSelected(ItemData selectedData)
        {
            _currentSelectedItem = selectedData;
            confirmButton.gameObject.SetActive(true);
            Debug.Log($"Item selected:{selectedData.ItemName}");
        }

        private void OnConfirmClicked()
        {
            if (_currentSelectedItem == null) return;
            UiManager.Instance.HideWindow(WindowsId.ShopUI);
            
            if (IsCharacter(_currentSelectedItem.itemType))
            {
                UiManager.Instance.ShowWindow(WindowsId.ConfirmUICharacter);
                ConfirmUICharacter confirmUI = UiManager.Instance.GetWindow(WindowsId.ConfirmUICharacter) as ConfirmUICharacter;
                confirmUI?.SetupConfirmation(_currentSelectedItem);
            }
            else
            {
                UiManager.Instance.ShowWindow(WindowsId.ConfirmUI_Item);
                ConfirmUI_Item confirmUI = UiManager.Instance.GetWindow(WindowsId.ConfirmUI_Item) as ConfirmUI_Item;
                confirmUI?.SetupConfirmation(_currentSelectedItem);
            }
        }

        private void OnDestroy()
        {
            if (confirmButton != null) confirmButton.onClick.RemoveListener(OnConfirmClicked);
        }
    }
}