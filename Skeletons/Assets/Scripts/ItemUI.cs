using System;
using NaughtyAttributes;
using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ItemUI : MonoBehaviour
{
    public ItemData ItemData { get; private set; }
    public TextMeshProUGUI itemNameText;
    public Image itemIcon;
    public Button itemButton;
    
    public Action<ItemData>OnItemClicked;

    public void SetData(ItemData data)
    {
        ItemData = data;
        itemIcon.sprite = ItemData.Icon;
        itemNameText.text = ItemData.ItemName;
    }

    private void Start()
    {
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => OnItemClicked?.Invoke(ItemData));
        }
    }

    private void OnDestroy()
    {
        if (itemButton != null)
        {
            itemButton.onClick.RemoveAllListeners();
        }
    }
}
