using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class CompleteCanvas : MonoBehaviour
{
    [SerializeField] private GameObject completed;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject gameName;
    void Start()
    {
        Invoke("Complete", 1f);
    }

    void Complete()
    {
        completed.SetActive(true);
        Invoke("Credits", 16f);
    }

    void Credits()
    {
        completed.SetActive(false);
        credits.SetActive(true);
        Invoke("GameName", 35f);
    }

    void GameName()
    {
        credits.SetActive(false);
        gameName.SetActive(true);
        Invoke("MusicFade", 7f);
        Invoke("MainMenu", 8f);
    }
    void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    void MusicFade()
    {
        EventManager.OnMusicOut();
    }
}