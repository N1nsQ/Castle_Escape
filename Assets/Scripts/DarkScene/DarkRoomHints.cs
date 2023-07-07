using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoomHints : MonoBehaviour, IDataPersistence
{
    [SerializeField] private ShowGuidance showGuidance;
    [SerializeField] private GameObject lamps;
    [SerializeField] private GameObject middleDoor;
    [SerializeField] private GameObject goalDoor;
    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject startStory;

    private bool darkStory;

    public void LoadData(GameData data)
    {
        darkStory = data.darkStory;

    }

    public void SaveData(GameData data)
    {
        data.darkStory = darkStory;
    }
    private void OnEnable()
    {
        EventManager.FirstPartSolved += ThroneHints;
        EventManager.SecondPartSolved += MathHints;
    }

    private void Start()
    {
        if (!darkStory)
        {
            Invoke("StartStory", 1f);
        }
        Invoke("FirstHint", 180f);
    }

    private void StartStory()
    {
        float cliplenght = startStory.GetComponent<AudioSource>().clip.length;
        storyCanvas.SetActive(true);
        darkStory = true;
        Invoke("CloseStory", cliplenght +1);
    }

    private void CloseStory()
    {
        storyCanvas.SetActive(false);
    }

    private void FirstHint()
    {
        if (!lamps.activeInHierarchy)
        {
            showGuidance.SetUpGuidance("Maybe check your inventory for help?");
            Invoke("Close", 10f);
            Invoke("SecondHint", 120f);
        }
    }

    private void SecondHint()
    {
        if (!lamps.activeInHierarchy)
        {
            showGuidance.SetUpGuidance("Open inventory with I and try getting torch.");
            Invoke("Close", 10f);
        }
    }

    private void ThroneHints()
    {
        Invoke("ThirdHint", 120f);
    }
    private void ThirdHint()
    {
        if (middleDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("Is there some picture to help with thrones?");
            Invoke("Close", 10f);
            Invoke("FourthHint", 120f);
        }
    }

    private void FourthHint()
    {
        if (middleDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("The arrows on the wall show how to turn thrones. Down means facing you.");
            Invoke("Close", 10f);
        }
    }

    private void MathHints()
    {
        Invoke("FifthHint", 120f);
    }

    private void FifthHint()
    {
        if (goalDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("The colors in the numbers might have a meaning.");
            Invoke("Close", 10f);
            Invoke("SixthHint", 120f);
        }
    }

    private void SixthHint()
    {
        if (goalDoor.tag != "goal")
        {
            showGuidance.SetUpGuidance("Do the calculations in the first room and change the same colored number to match the answer.");
            Invoke("Close", 10f);
            Invoke("SixthHint", 120f);
        }
    }

    private void Close()
    {
        showGuidance.CloseGuidance();
    }

    private void OnDisable()
    {
        EventManager.FirstPartSolved -= ThroneHints;
        EventManager.SecondPartSolved -= MathHints;
    }
}
