using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DarkRoomPuzzleConditions : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject throne1;
    [SerializeField] private GameObject throne2;
    [SerializeField] private GameObject throne3;
    [SerializeField] private GameObject throne4;
    [SerializeField] private GameObject throne5;

    [SerializeField] private TextMeshProUGUI solution1;
    [SerializeField] private TextMeshProUGUI solution2;
    [SerializeField] private TextMeshProUGUI solution3;
    [SerializeField] private TextMeshProUGUI solution4;

    [SerializeField] private GameObject goalDoor;
    [SerializeField] private GameObject middleDoor;

    private bool darkMiddle;
    private bool darkGoal;

    public void LoadData(GameData data)
    {
        darkMiddle = data.darkMiddle;
        darkGoal = data.darkGoal;
    }

    public void SaveData(GameData data)
    {
        data.darkMiddle = darkMiddle;
        data.darkGoal = darkGoal;
    }

    private void Start()
    {
        if (darkMiddle)
        {
            middleDoor.tag = "goal";
            GameObject.Destroy(throne1);
            GameObject.Destroy(throne2);
            GameObject.Destroy(throne3);
            GameObject.Destroy(throne4);
            GameObject.Destroy(throne5);
        }
        if (darkGoal)
        {
            goalDoor.tag = "goal";
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (middleDoor.tag != "goal") ThroneCheck();
        if (middleDoor.tag == "goal") MathPuzzleCheck();
    }

    private void ThroneCheck ()
    {
        if (throne1.transform.eulerAngles.y == 90 &&
            throne2.transform.eulerAngles.y == 180 &&
            throne3.transform.eulerAngles.y == 0 &&
            throne4.transform.eulerAngles.y == 270 &&
            throne5.transform.eulerAngles.y == 0)
        {
            Debug.Log("thrones right");
            middleDoor.tag = "goal";
            EventManager.OnSecondPartSolved();

            darkMiddle = true;

            GameObject.Destroy(throne1);
            GameObject.Destroy(throne2);
            GameObject.Destroy(throne3);
            GameObject.Destroy(throne4);
            GameObject.Destroy(throne5);
        }
    }


    private void MathPuzzleCheck()
    {
        if (solution1.text == "4" &&
            solution2.text == "0" &&
            solution3.text == "7" &&
            solution4.text == "9")
        {
            goalDoor.tag = "goal";
            darkGoal = true;
            ShowingInstructions.OnShowCompeleted();
            Debug.Log("Math solved");
            GameObject.Destroy(this);
        }
    }
}
