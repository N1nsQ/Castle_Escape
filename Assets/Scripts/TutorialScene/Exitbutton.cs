using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class Exitbutton : MonoBehaviour
{
    [SerializeField] private GameObject torch;
    public void CloseAll()
    {
        torch.SetActive(false);
    }
}
