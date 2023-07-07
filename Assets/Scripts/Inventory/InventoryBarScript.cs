using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InventoryBarScript : MonoBehaviour
{
    public GameObject m_slotPrefab;
    public InventorySystem inventory;

    private void Start()
    {
        gameObject.SetActive(false);      
    }

    private void OnEnable()
    {
        InventorySystem.OnInventoryChanged += OnUpdateInventory;
    }

    private void OnUpdateInventory(bool show)
    {
        if (this != null)
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }

            DrawInventory();
            if (show)
            {
                gameObject.SetActive(true);
                Invoke("HideInventory", 1f);
            }
        }
    }

    void HideInventory()
    {
        gameObject.SetActive(false);
    }

    private void DrawInventory()
    {
        foreach(InventoryItem item in inventory.Container.Items)
         {
             AddInventorySlot(item);
         }

    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        SlotItemScript slot = obj.GetComponent<SlotItemScript>();
        slot.Set(item, inventory);
    }
    /*
    private void OnDisable()
    {
        InventorySystem.OnInventoryChanged -= OnUpdateInventory;
    }*/

}
