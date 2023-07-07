using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTouchFloorScript : MonoBehaviour  
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            EventManager.OnBoxStucked();
            Destroy(gameObject, 20f); 
        }
    }
}
