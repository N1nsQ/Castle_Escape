using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlScript : MonoBehaviour
{
    private List<string> objectsInBowl;

    private void Awake()
    {
        objectsInBowl = new List<string>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GreenBag" || other.gameObject.name == "OrangeBag" || other.gameObject.name == "PurpleBag")
        {
            gameObject.tag = other.gameObject.name;
            objectsInBowl.Add(other.gameObject.name);
            Debug.Log(gameObject.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInBowl.Remove(other.gameObject.name);
        if (objectsInBowl.Count > 0) gameObject.tag = objectsInBowl[0];
        else gameObject.tag = "Untagged";
        Debug.Log(gameObject.tag);
    }

   
}
