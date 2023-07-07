using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeableNumber : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private TextMeshProUGUI _canvasText;
    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        int num = int.Parse(_canvasText.text);
        if (num < 9)
        {
            num++;
        }
        else
        {
            num = 0;
        }
        _canvasText.text = num.ToString(); ;

        return true;
    }
}
