using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
//using static UnityEditor.Progress;

public class TriggerTutorial2 : MonoBehaviour
{
    [SerializeField] private ShowGuidance showGuidance;
    private bool showBarrel = true;

    public bool Interact(Player interactor)
    {
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (showBarrel && other.gameObject.tag == "Player")
        {
            showGuidance.SetUpGuidance("Maybe try to move that barrel");
            showBarrel = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        showGuidance.CloseGuidance();
    }
}
