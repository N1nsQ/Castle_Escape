using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GetInventoryItem : MonoBehaviour
{

    [SerializeField] private GameObject meat;
    [SerializeField] private GameObject meat2;
    [SerializeField] private GameObject mug;
    [SerializeField] private GameObject mug2;
    [SerializeField] private GameObject scroll;
    [SerializeField] private GameObject carrot;
    [SerializeField] private GameObject carrot2;
    [SerializeField] private GameObject carrot3;
    [SerializeField] private GameObject bread;
    [SerializeField] private GameObject bread2;
    [SerializeField] private GameObject bread3;
    private GameObject selectedObject;
    [SerializeField] private InventorySystem inventory;

    public GameObject GetItem(string name)
    {
        if (name == "Meat")
        {
            if (inventory.Container.Items.Find(x => x.ID == 7).StackSize == 2)
            {
                selectedObject = meat;
            }
            else
            {
                selectedObject = meat2;
            }
        }
        if (name == "Mug of Beer")
        {
            if (inventory.Container.Items.Find(x => x.ID == 7).StackSize == 2)
            {
                selectedObject = mug;
            }
            else
            {
                selectedObject = mug2;
            }
        }
        if (name == "Carrot")
        {
            Debug.Log(inventory.Container.Items.Find(x => x.ID == 5).StackSize);
            if (inventory.Container.Items.Find(x => x.ID == 5).StackSize == 2)
            {
                selectedObject = carrot;
            }
            if (inventory.Container.Items.Find(x => x.ID == 5).StackSize == 1)
            {
                selectedObject = carrot2;
            }
            else
            {
                selectedObject = carrot3;
            }
        }

        if (name == "Bread")
        {
            Debug.Log(inventory.Container.Items.Find(x => x.ID == 4).StackSize);
            if (inventory.Container.Items.Find(x => x.ID == 4).StackSize == 2)
            {

                selectedObject = bread;
            }
            if (inventory.Container.Items.Find(x => x.ID == 4).StackSize == 1)
            {

                selectedObject = bread2;
            }
            else
            {
                selectedObject = bread3;
            }
        }
        if (name == "Scroll")
        {
            EventManager.OnShowStory();
        }
        return selectedObject;
    }
}
