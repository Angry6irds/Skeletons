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
        [SerializeField, TextArea(3, 5)] string statsDescription;
        [SerializeField] GameObject unitPrefab;
        
        public Sprite Icon => icon;
        
        public string ItemName => itemName;

        public string StatsDescription => statsDescription;

        public GameObject UnitPrefab => unitPrefab;
    
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