using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemScript : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private TextMeshProUGUI m_label;
    [SerializeField] private GameObject m_stackObj;
    [SerializeField] private TextMeshProUGUI m_stackLabel;

    public void Set(InventoryItem item, InventorySystem inventory)
    {
        m_icon.sprite = inventory.database.GetItem[item.ID].icon;
        m_label.text = inventory.database.GetItem[item.ID].displayName;
        if(item.StackSize <= 1)
        {
            m_stackObj.SetActive(false);
            return;
        }
        m_stackLabel.text = item.StackSize.ToString();
    }
    
}
