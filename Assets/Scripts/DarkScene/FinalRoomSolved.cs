using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FinalRoomSolved : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI solution1;
    [SerializeField] private TextMeshProUGUI solution2;
    [SerializeField] private TextMeshProUGUI solution3;
    [SerializeField] private TextMeshProUGUI solution4;
    [SerializeField] private TextMeshProUGUI solution5;
    [SerializeField] private TextMeshProUGUI solution6;
    [SerializeField] private TextMeshProUGUI solution7;
    [SerializeField] private GameObject goalDoor;

    void Update()
    {
        PuzzleCheck();
    }

    private void PuzzleCheck()
    {
        if (solution1.text == "3" &&
            solution2.text == "5" &&
            solution3.text == "9" &&
            solution4.text == "3" &&
            solution5.text == "5" &&
            solution6.text == "7" &&
            solution7.text == "Yes")
        {
            goalDoor.tag = "goal";
            Debug.Log("Puzzle solved");
            GameObject.Destroy(this);
        }
    }
}
