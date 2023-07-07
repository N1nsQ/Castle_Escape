using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformComplete : MonoBehaviour
{
    [SerializeField] private GameObject goldenKey;
    [SerializeField] private GameObject goalDoor;
    [SerializeField] private GameObject candle;
    private void OnEnable()
    {
        goldenKey.SetActive(true);
        candle.SetActive(true);
        if (goalDoor.tag != "goal" )
        {
            ShowingInstructions.OnShowCompeleted();
        }
    }

    private void OnDisable()
    {
        goldenKey.SetActive(false);
        candle.SetActive(false);
    }
}
