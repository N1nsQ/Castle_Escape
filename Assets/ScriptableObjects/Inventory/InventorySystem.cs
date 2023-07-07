using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "InventorySystem/Inventory")]
public class InventorySystem : ScriptableObject
{
    public ItemDatabaseObject database;
    public InventoryUsed Container;
    public static UnityAction<bool> OnInventoryChanged;

    public void AddItem(Item _item)
    {
        
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].Item.Id == _item.Id)
            {
                Container.Items[i].AddToStack();
                if (_item.Name == "Scroll")
                {
                    EventManager.OnShowOneStory();
                    OnInventoryChanged?.Invoke(false);
                }
                else OnInventoryChanged?.Invoke(true);
                return;
            }
        }
        Container.Items.Add(new InventoryItem(_item.Id, _item));
        if (_item.Name == "Scroll")
        {
            EventManager.OnShowOneStory();
            OnInventoryChanged?.Invoke(false);
        }
        else if (_item.Name == "Diary")
        {
            EventManager.OnShowDiary();
            OnInventoryChanged?.Invoke(false);
        }
        else if (_item.Name == "Torch")
        {
            OnInventoryChanged?.Invoke(false);
        }
        else OnInventoryChanged?.Invoke(true);
    }

    public void RemoveItem(ItemCollectable _item)
    {
        var itemToRemove = Container.Items.Find(item => item.ID == _item.item.Id);

        if (itemToRemove.StackSize > 1)
        {
            itemToRemove.RemoveFromStack();
        }
        else
        {
            Container.Items.Remove(itemToRemove);
        }
        OnInventoryChanged?.Invoke(false);
    }
    
    public List<InventoryItem> getItems()
    {
        return Container.Items;
    }
}

[System.Serializable]
public class InventoryUsed
{
    public List<InventoryItem> Items = new List<InventoryItem>();
}

[System.Serializable]
public class InventoryItem
{
    public int ID;
    public Item Item;
    public int StackSize;

    public InventoryItem(int _id, Item source)
    {
        ID = _id;
        Item = source;
        AddToStack();
    }

    public void AddToStack()
    {
        StackSize++;
    }

    public void RemoveFromStack()
    {
        StackSize--;
    }
}