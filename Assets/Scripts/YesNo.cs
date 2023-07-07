using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNo : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private TextMeshProUGUI _canvasText;
    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        string answer = _canvasText.text;
        if (answer == "Yes")
        {
            answer = "No";
        }
        else
        {
            answer = "Yes";
        }
        _canvasText.text = answer;

        return true;
    }
}
