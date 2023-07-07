using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
[System.Serializable]
public class InventoryItemData : ScriptableObject
{
    public int Id;
    public string name_id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;

    public Item(InventoryItemData item)
    {
        Name = item.name;
        Id = item.Id;
    }
}
