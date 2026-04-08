using NaughtyAttributes;
using ScriptableObjet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ItemUI : MonoBehaviour
{
    public ItemData ItemData;
    public TextMeshProUGUI itemNameText;
    public Image itemIcon;
    
    [Button]
    public void SetsData()
    {
        itemIcon.sprite = ItemData.Icon;
        itemNameText.text = ItemData.name;
    }
}
