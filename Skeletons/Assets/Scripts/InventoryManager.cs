using System;
using System.Collections.Generic;
using Alejandro;
using NaughtyAttributes;
using ScriptableObjet;
using UnityEngine;

[Serializable]
public class RuntimeItem
{
    public string name;
    public ItemType itemType;
    
    public RuntimeItem(string name, ItemType itemType)
    {
        this.name = name;
        this.itemType = itemType;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public List<RuntimeItem> items = new List<RuntimeItem>(); 
    public List<ItemData> itemDatas = new List<ItemData>();

    public Action OnInventoryChanged;

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
        LoadInventory();
    }

    public void AddItem(ItemData item)
    {
        if (HasItem(item.itemType)) return;
        
        RuntimeItem runtimeItem = new RuntimeItem(item.ItemName, item.itemType);
        items.Add(runtimeItem);
        SaveInventory();
        OnInventoryChanged?.Invoke();
    }

    public bool HasItem(ItemType type)
    {
        foreach (var item in items)
        {
            if (item.itemType == type) return true;
        }
        return false;
    }

    public ItemData GetItemData(ItemType type)
    {
        foreach (var data in itemDatas)
        {
            if (data.itemType == type) return data;
        }
        return null;
    }

    [Button]
    public void SaveInventory()
    {
        string json = JsonHelper.ToJson(items.ToArray(), true);
        string path = Application.persistentDataPath + "/inventory.json";
        System.IO.File.WriteAllText(path, json);
    }
    
    public void LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.json";
        if(!System.IO.File.Exists(path)) return;
        
        string json = System.IO.File.ReadAllText(path);
        RuntimeItem[] loadItems = JsonHelper.FromJson<RuntimeItem>(json);
        
        items.Clear();
        if (loadItems != null)
        {
            items.AddRange(loadItems);
        }
        OnInventoryChanged?.Invoke();
    }
}