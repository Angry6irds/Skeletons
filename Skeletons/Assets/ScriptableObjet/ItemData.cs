using UnityEngine;

namespace ScriptableObjet
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjet/ItemData")]
    public class ItemData:ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] string itenName;
        public Sprite Icon => icon;
        public string ItenName => itenName;

    }
}