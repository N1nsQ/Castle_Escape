using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;

public class TableScript : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject goalDoor;
    private int carrots = 0;
    private int bread = 0;
    private int mug = 0;
    private int meat = 0;

    private bool sokkeloSolved;
    public void LoadData(GameData data)
    {
        sokkeloSolved = data.sokkeloPuzzle;
    }
    public void SaveData(GameData data)
    {
        data.sokkeloPuzzle = sokkeloSolved;
    }

    private void Start()
    {
        if (sokkeloSolved)
        {
            goalDoor.tag = "goal";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if ( other.gameObject.tag != "Player")
        {
            if (other.gameObject.name.Contains("Carrot")) carrots += 1;
            if (other.gameObject.name.Contains("Bread")) bread += 1;
            if (other.gameObject.name.Contains("Mug")) mug += 1;
            if (other.gameObject.name.Contains("Meat")) meat += 1;
            other.gameObject.tag = "Untagged";
            other.gameObject.name = "TableItem";
        }

        if (carrots >= 2 && bread >= 2 && mug >= 1 && meat >= 1 && goalDoor.tag != "goal")
        {
            goalDoor.gameObject.tag = "goal";
            sokkeloSolved = true;
            ShowingInstructions.OnShowCompeleted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.tag = "PointOfInterest";
    }
}
