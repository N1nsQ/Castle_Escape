using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enablemouse : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
