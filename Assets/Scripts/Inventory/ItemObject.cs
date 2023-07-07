using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

[RequireComponent(typeof(UniqueID))]
public class ItemObject : MonoBehaviour, IDataPersistence
{
    private InventoryItemData referenceItem;
    private string uniqueId;
    private ItemPickUpSaveData itemSaveData;
    
    public void Awake()
    {
        uniqueId = GetComponent<UniqueID>().ID;
        referenceItem = GetComponent<ItemCollectable>().item;
        itemSaveData = new ItemPickUpSaveData(referenceItem.Id, transform.position, transform.rotation);
    }

    public void Start()
    {
        if (Player.activeItems.ContainsKey(uniqueId))
        {
            Player.activeItems[uniqueId] = itemSaveData;
        }
        else
        {
            Player.activeItems.Add(uniqueId, itemSaveData);
        }
        Debug.Log(uniqueId);
    }

    public void SaveData(GameData data)
    {
        if (this == null)
        {
            if (!data.collectedItems.Contains(uniqueId))
            {
                data.collectedItems.Add(uniqueId);
            }
        }
        else
        {
            data.collectedItems.Remove(uniqueId);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.collectedItems.Contains(uniqueId)) Destroy(this.gameObject);
    }
    
    public void OnHandlePickupItem()
    {
        Debug.Log("I collected it");
        if (Player.activeItems.ContainsKey(uniqueId)) Player.activeItems.Remove(uniqueId);
        //if (referenceItem.displayName == "Scroll") EventManager.OnShowOneStory();
        Destroy(gameObject);
    }

    public void OnHandleTakeItemFromInv()
    {
        Debug.Log("Deleted it from inventory");
        if (Player.activeItems.ContainsKey(uniqueId))
        {
            Player.activeItems[uniqueId] = itemSaveData;
        }
        else
        {
            Player.activeItems.Add(uniqueId, itemSaveData);
        }
    }

}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public int id;
    public Vector3 position;
    public Quaternion rotation;

    public ItemPickUpSaveData(int _id, Vector3 _position, Quaternion _rotation)
    {
        id = _id;
        position = _position;
        rotation = _rotation;
    }
}