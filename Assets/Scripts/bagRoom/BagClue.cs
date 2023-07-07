using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagClue : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private TextMeshProUGUI _canvasText;
    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        string answer = _canvasText.text;
        if (answer == "Help")
        {
            answer = "When you mix the big color with it's big neighbor, what color should there be in the pot?";
        }
        _canvasText.text = answer;

        return true;
    }
}