using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenKey : MonoBehaviour
{
    [SerializeField] private ShowGuidance showGuidance;

    private void OnTriggerEnter(Collider other)
    {
        showGuidance.SetUpGuidance("This key seems different than others! Is this gold?");
    }

    private void OnTriggerExit(Collider other)
    {
        showGuidance.CloseGuidance();
    }
}
