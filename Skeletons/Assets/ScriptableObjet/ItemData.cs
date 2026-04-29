using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjet
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjet/ItemData")]
    public class ItemData:ScriptableObject
    {
        [SerializeField] Sprite icon;
        [FormerlySerializedAs("itenName")] [SerializeField] string itemName;
        [SerializeField] public ItemType itemType;
        
        public Sprite Icon => icon;
        
        public string ItemName => itemName;
    
    }

    public enum ItemType
    {
        skeleton,
        iceSkeleton,
        mudSkeleton,
        sword,
        axe,
    }
}