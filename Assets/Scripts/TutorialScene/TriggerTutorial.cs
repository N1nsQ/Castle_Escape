using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
//using static UnityEditor.Progress;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] private ShowGuidance showGuidance;
    private bool instruct = true;

    public bool Interact(Player interactor)
    {
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (instruct)
        {
            showGuidance.SetUpGuidance("Press H to see instructions");
            instruct = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        showGuidance.CloseGuidance();
    }
}
