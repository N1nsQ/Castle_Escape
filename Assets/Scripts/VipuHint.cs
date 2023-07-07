using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VipuHint : MonoBehaviour
{
    [SerializeField] private ShowGuidance guidance;
    [SerializeField] private GameObject fence;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Instructions", 60f);
        Invoke("ShowHint", 300f);

    }


    // Update is called once per frame
    void Update()
    {


    }

    private void Instructions()
    {
        guidance.SetUpGuidance("There are 6 switches in the room. Use hints to figure out the right direction to those switces.");
        Invoke("Close", 10f);
    }

    private void ShowHint()
    {
        guidance.SetUpGuidance("What goes up? What goes down? Look at the images and try to think where you normally find these things.");
        Invoke("Close", 10f);
    }

    private void Close()
    {
        guidance.CloseGuidance();
    }
}

