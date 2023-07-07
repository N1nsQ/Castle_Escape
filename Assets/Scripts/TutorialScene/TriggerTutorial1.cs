using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
//using static UnityEditor.Progress;

public class TriggerTutorial1 : MonoBehaviour
{
    [SerializeField] private ShowGuidance showGuidance;
    private bool key = true;

    public bool Interact(Player interactor)
    {
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (key)
        {
            showGuidance.SetUpGuidance("Is there something up there");
            key = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        showGuidance.CloseGuidance();
    }
}
