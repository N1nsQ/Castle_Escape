using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using static UnityEditor.Progress;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public long lastUpdated;
    public string currentScene;

    // Puzzleroom events:
    // Tutorial
    public bool firstStory;
    public bool diaryDecryped;
    public bool TorchFirst;
    // Dark Room
    public bool darkStory;
    public bool lightsOn;
    public bool darkMiddle;
    public bool darkGoal;
    // Vipu Scene
    public bool vipuPuzzle;
    // Sokkelo
    public bool sokkeloPuzzle;
    // Platform
    public bool platformPuzzle;
    public bool platformStory;
    //bags
    public bool bagsStory;
    public bool bagsSolved;

    public SerializableDictionary<string, bool> scenesCompleted;

   //Inventory saving
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickUpSaveData> activeItems;
    public List<InventoryItem> inventoryItems;

    // Material saving from customization
    public bool beard;
    public bool moustache;
    public Color shirt;
    public Color pants;
    public Color skin;

    public float timeToDisplay;

    // The values defined in this constructor will be the default values the game starts with when there's no data to load
    public GameData()
    {
        // Here should be the player starting point value: -5.7, 0.2500001, -9.93
        playerPosition = new Vector3((float)-5.7, (float)0.2500001, (float)-9.93);
        playerRotation = new Quaternion((float)0.00000, (float)0.65060, (float)0.00000, (float)0.75942);
        scenesCompleted = new SerializableDictionary<string, bool>();

        //Inventory saving
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickUpSaveData>();
        inventoryItems = new List<InventoryItem>();
        TorchFirst = true;
        timeToDisplay = 900f;

        // Puzzle events
        darkStory = false;
        lightsOn = false;
        darkMiddle = false;
        darkGoal = false;
        vipuPuzzle = false;
        sokkeloPuzzle = false;
        platformPuzzle = false;
        platformStory = false;
        bagsStory = false;
        bagsSolved = false;

    }
}
