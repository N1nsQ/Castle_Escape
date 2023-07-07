//using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Decrypt : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject resolve;
    [SerializeField] private GameObject instructions;

    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        if (!instructions.activeSelf)
        {
            resolve.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }
}
