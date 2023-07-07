using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject gameSuccessMenu;

    void Start()
    {
        EventManager.OnTimerStart();
        //Invoke("DisableText", 5f);
    }
    /*
    void DisableText()
    {
        hint.enabled = false;
    }*/

    private void OnEnable()
    {
        EventManager.TimerStop += EventManagerOnTimeStop;
        EventManager.Success += EventManagerOnGameOver;
    }

    private void EventManagerOnGameOver()
    {
        gameSuccessMenu.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.TimerStop -= EventManagerOnTimeStop;
        EventManager.Success -= EventManagerOnGameOver;
    }

    private void EventManagerOnTimeStop()
    {
        gameOverMenu.SetActive(true);
    }
}