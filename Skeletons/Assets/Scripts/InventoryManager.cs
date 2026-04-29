using System;
using System.Collections.Generic;
using Alejandro;
using NaughtyAttributes;
using ScriptableObjet;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
    public class RuntimeItem
    {
        public string name;
        public Sprite sprite;
        
        public  RuntimeItem(string name, Sprite sprite)
        {
            this.name = name;
            this.sprite = sprite;
        }
    }
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance{get; private set;}
        public List<RuntimeItem> items = new List<RuntimeItem>(); 
        public List<ItemData> itemDatas = new List<ItemData>();
        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void CreateItem(ItemType item)
        {
            foreach (var Item in  itemDatas )
            {
                if (Item.itemType == item)
                {
                    RuntimeItem runtimeItem = new RuntimeItem(Item.name, Item.Icon);
                    items.Add(runtimeItem);
                }
            }
        }
        [Button]
        public void CreateItemTest()
        {
            CreateItem(ItemType.axe);
            Debug.Log("CreateItemTest:" + items.Count);
        }
        
        [Button]
        public void SafeInvetoryInJson()
        {
            //Create file
            string json = JsonHelper.ToJson(items.ToArray(), true);
            Debug.Log(json);
            string path = Application.persistentDataPath + "/inventory.json";
            System.IO.File.WriteAllText(path, json);
            Debug.Log(path);
        }
    }
    