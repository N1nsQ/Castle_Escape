using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCondition : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject bowlOrange;
    [SerializeField] private GameObject bowlPurple;
    [SerializeField] private GameObject bowlGreen;
    [SerializeField] private GameObject potionBowl;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject goalDoor;
    [SerializeField] private GameObject canvasStart;
    [SerializeField] private GameObject starttext;
    private bool storyShown = false;
    private bool solved = false;

    private void Awake()
    {
        potionBowl.SetActive(false);
        key.SetActive(false);
    }

    private void Start()
    {
        if (!storyShown)
        {
            canvasStart.SetActive(true);
            starttext.SetActive(true);
            storyShown = true;
            Invoke("Close", 5f);
        }
    }

    private void Close()
    {
        canvasStart.SetActive(false);
        starttext.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (bowlGreen.gameObject.tag == "GreenBag" && bowlOrange.gameObject.tag == "OrangeBag" && bowlPurple.gameObject.tag == "PurpleBag")
        {
            Debug.Log("done");
            potionBowl.SetActive(true);
            key.SetActive(true);
            goalDoor.tag = "goal";
            ShowingInstructions.OnShowCompeleted();
            Destroy(gameObject);
            solved = true;
        }
        else if (solved)
        {
            potionBowl.SetActive(true);
            key.SetActive(true);
            goalDoor.tag = "goal";
            Destroy(gameObject);
        }
            
    }

    public void LoadData(GameData data)
    {
        storyShown = data.bagsStory;
        solved = data.bagsSolved;
    }

    public void SaveData(GameData data)
    {
        data.bagsStory = storyShown;
        data.bagsSolved = solved;
    }
}
