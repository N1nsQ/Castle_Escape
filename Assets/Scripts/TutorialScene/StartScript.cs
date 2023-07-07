using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class StartScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject startstory;
    [SerializeField] private GameObject torch;
    [SerializeField] private ShowGuidance showGuidance;

    private bool storyShown = false;

    private void Start()
    {
        if (!storyShown)
        {
            Invoke("Story", 2f);
        }
    }

    private void OnEnable()
    {
        EventManager.TakeTorch += OnTakeTorch;
    }
    private void OnTakeTorch()
    {
        Debug.Log("here");
        torch.SetActive(true);
        Invoke("CloseTorch", 25f);
    }

    private void CloseTorch()
    {
        torch.SetActive(false);
    }

    private void Story()
    {
        startstory.SetActive(true);
        storyShown = true;
    }

    public void SaveData(GameData data)
    {
        data.firstStory = storyShown;
    }

    public void LoadData(GameData data)
    {
        storyShown = data.firstStory;
    }
    private void OnDisable()
    {
        EventManager.TakeTorch -= OnTakeTorch;
    }

}
