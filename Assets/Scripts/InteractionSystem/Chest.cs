using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject goalDoor;
    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        // Testing tag adding
        goalDoor.tag = "goal";
        Debug.Log("Goal Door can be opened!");
        return true;
    }
}
