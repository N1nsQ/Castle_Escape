using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SokkeloScrpit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowingInstructions.OnShowInstructions();
            Debug.Log("instructions");
            Destroy(gameObject);
        }
    }
}
