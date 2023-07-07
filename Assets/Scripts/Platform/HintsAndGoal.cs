using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsAndGoal : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject goalDoor;
    [SerializeField] private GameObject keyPlatform;
    [SerializeField] private ShowGuidance showGuidance;
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject startStory;
    [SerializeField] private GameObject goldKey;

    private bool platformPuzzle;
    private bool platformStory;

    /*
    private void OnEnable()
    {
        EventManager.PlatformUnActivated += HideKey;
    }

    private void OnDisable()
    {
        EventManager.PlatformUnActivated -= HideKey;
    }*/

    public void LoadData(GameData data)
    {
        platformPuzzle = data.platformPuzzle;
        platformStory = data.platformStory;
    }

    public void SaveData(GameData data)
    {
        data.platformPuzzle = platformPuzzle;
        data.platformStory = platformStory;
    }
    // Start is called before the first frame update
    private void Start()
    {
        if (platformPuzzle)
        {
            goalDoor.tag = "goal";
            keyPlatform.SetActive(true);
        }
        if (platformPuzzle == false)
        {
            Invoke("ShowFirstHint", 360f);
        }
        if (!platformStory)
        {
            Invoke("StartStory", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPlatform.gameObject.activeSelf && goalDoor.tag != "goal")
        {
            platformPuzzle = true;
            goalDoor.tag = "goal";
            Invoke("ShowSecondHint", 360f);
        }

    }

    private void ShowFirstHint()
    {
        if (platform.activeSelf == false)
        {
            showGuidance.SetUpGuidance("Some of the switches toggle one platform, some toggle two.");
            Invoke("CloseGuidance", 7);
        }
    }

    private void ShowSecondHint()
    {
        showGuidance.SetUpGuidance("Remember to find the key, if you didn't already!");
        Invoke("CloseGuidance", 7);
    }

    private void CloseGuidance()
    {
        showGuidance.CloseGuidance();
    }

    /*
    private void HideKey()
    {
        goldKey.SetActive(false);
    }*/

    private void StartStory()
    {
        float cliplenght = startStory.GetComponent<AudioSource>().clip.length;
        storyCanvas.SetActive(true);
        platformStory = true;
        Invoke("CloseStory", cliplenght + 1);
    }
    private void CloseStory()
    {
        storyCanvas.SetActive(false);
    }
}
