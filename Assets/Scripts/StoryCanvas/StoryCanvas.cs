using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class StoryCanvas : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject storyImage;
    [SerializeField] Button next;
    [SerializeField] Button prev;

    [SerializeField] private GameObject diary;
    [SerializeField] private GameObject diaryText;
    [SerializeField] private GameObject diaryText2;
    [SerializeField] private GameObject diaryText3;
    [SerializeField] private GameObject diaryImage;
    [SerializeField] private bool diaryDecryped = false;
    [SerializeField] private GameObject gameOverMenu;

    private void OnEnable()
    {
        EventManager.ShowStory += Storyrequested;
        EventManager.ShowOneStory += ShowOneStory;
        EventManager.ShowDiary += OnShowDiary;
        EventManager.DiaryDecrypted += OnDiaryDecrypted;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventManager.TimerStop += EventManagerOnTimeStop;
    }

    void Start()
    {
        EventManager.OnTimerStart();
    }

    private void OnDiaryDecrypted()
    {
        diaryDecryped = true;
        OnShowDiary();
    }

    public void SaveData(GameData data)
    {
        data.diaryDecryped = diaryDecryped;
    }

    public void LoadData(GameData data)
    {
        diaryDecryped = data.diaryDecryped;
    }

    private void OnShowDiary()
    {
        diary.SetActive(true);
        if (!diaryDecryped)
        {
            diaryText.SetActive(true);
            diaryText2.SetActive(false);
            diaryText3.SetActive(false);
            diaryImage.SetActive(false);
            Invoke("Exit", 12f);
        }
        else
        {
            CancelInvoke();
            diaryText.SetActive(false);
            diaryText2.SetActive(true);
            diaryText3.SetActive(false);
            diaryImage.SetActive(true);
            Invoke("NextPage", 18f);
        }
    }

    private void NextPage()
    {
        diaryText.SetActive(false);
        diaryText2.SetActive(false);
        diaryText3.SetActive(true);
        Invoke("Exit", 29f);
    }

    private void Storyrequested()
    {
        storyImage.SetActive(true);
        Debug.Log("Many stories request");
    }

    private void OnDisable()
    {
        EventManager.ShowStory -= Storyrequested;
        EventManager.ShowOneStory -= ShowOneStory;
        EventManager.ShowDiary -= OnShowDiary;
        EventManager.DiaryDecrypted -= OnDiaryDecrypted;
        EventManager.TimerStop -= EventManagerOnTimeStop;
    }
    private void ShowOneStory()
    {
        CancelInvoke(); 
        storyImage.SetActive(true);
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        Debug.Log("Story request");
        Invoke("CloseThis", 33.0f);
    }
    void CloseThis()
    {
        storyImage.SetActive(false);
    }
    public void Next()
    {
        StoryScript.currentlyShown++;
        Debug.Log("Next");
    }

    public void Previous()
    {
        StoryScript.currentlyShown--;
        Debug.Log("Previous");
    }

    public void Exit()
    {
        storyImage.SetActive(false);
        diary.SetActive(false);
    }

    private void EventManagerOnTimeStop()
    {
        gameOverMenu.SetActive(true);
        Invoke("RestartLevel", 7f);

    }

    private void RestartLevel()
    {
        DataPersistenceManager.instance.SetRestartLevel(true);
        gameOverMenu.SetActive(false);
        SceneManager.LoadSceneAsync("CastleEditedScene");
        DataPersistenceManager.instance.SaveGame();
    }
}