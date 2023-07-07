using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasInventory : MonoBehaviour
{
    public GameObject inventoryBar;
    private Button selectedItem;

    private void OnEnable()
    {
       EventManager.OpenInventory += EventManagerOnOpenInventory;
       EventManager.CloseInventory += EventManagerOnCloseInventory;
    }

    private void EventManagerOnCloseInventory()
    {
        if (inventoryBar.activeSelf)
        {
            inventoryBar.SetActive(false);
            //Time.timeScale = 1;
        }
        return;
    }

    private void EventManagerOnOpenInventory()
    {
        if (inventoryBar.activeSelf)
        {
            inventoryBar.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            inventoryBar.SetActive(true);
            Time.timeScale = 0;
            Debug.Log(gameObject.transform.GetChild(0).childCount);
            if (gameObject.transform.GetChild(0).childCount > 0)
            {
                selectedItem = gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Button>();
                selectedItem.Select();
                selectedItem.OnSelect(null);
            }
        }
    }

    private void OnDisable()
    {
        EventManager.OpenInventory -= EventManagerOnOpenInventory;
        EventManager.CloseInventory -= EventManagerOnCloseInventory;
    }
}