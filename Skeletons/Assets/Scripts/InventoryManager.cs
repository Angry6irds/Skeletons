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
        public ItemType itemType;
        public Sprite sprite;
        
        public  RuntimeItem(string name, Sprite sprite, ItemType itemType)
        {
            this.name = name;
            this.sprite = sprite;
            this.itemType = itemType;
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

        void Start()
        {
            LoadInvetory();
        }

        public void CreateItem(ItemType item)
        {
            foreach (var Item in  itemDatas )
            {
                if (Item.itemType == item)
                {
                    RuntimeItem runtimeItem = new RuntimeItem(Item.name, Item.Icon, Item.itemType);
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
        public void CreateItemTest2()
        {
            CreateItem(ItemType.sword);
            Debug.Log("CreateItemTest2:" + items.Count);
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
        
        public void LoadInvetory()
        {
            string path = Application.persistentDataPath + "/inventory.json";
            if(!System.IO.File.Exists(path))
            {
                Debug.LogError("no inventory file found:"  + path);
                return;
            }
            string json = System.IO.File.ReadAllText(path);
            RuntimeItem[] loadItems = JsonHelper.FromJson<RuntimeItem>(json);
            
            items.Clear();
            items.AddRange(loadItems);
            
        }
        
    }
    