using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectingInventoryItem : MonoBehaviour
{ 
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        var allKids = GetComponentsInChildren<Transform>();
        var label = allKids.Where(k => k.gameObject.name == "Label").FirstOrDefault();
        Debug.Log(label.GetComponent<TextMeshProUGUI>().text);
        EventManager.OnGetInventoryItem(label.GetComponent<TextMeshProUGUI>().text);
        EventManager.OnCloseInventory();

    }

}
