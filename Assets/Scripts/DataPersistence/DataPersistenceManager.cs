using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initalizeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";


    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private bool newPuzzleLevelStarted = false;
    private bool restartInvoked = false;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled. Enable from DataPersistenceManager object.");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);

        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileId);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        // Update the proile to use for saving and loading
        this.selectedProfileId = newProfileId;

        // Load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // Return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        // Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId, restartInvoked);

        // Start a new game if the data is null and we've configured to initialize data for debugging purposes
        if (this.gameData == null && initalizeDataIfNull)
        {
            NewGame();
        }

        // If no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // Push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        restartInvoked = false;
        Debug.Log("State of restart invoked: " + restartInvoked);
    }

    public void SaveGame()
    {
        // Return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        // If we don't have any data, log a warning here
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // Pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        // Save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId, newPuzzleLevelStarted);
        Debug.Log("State of new puzzlestarted: " + newPuzzleLevelStarted);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }


    // Testing restartfile
    public void SetNewLevel(bool _newPuzzleLevelStarted)
    {
        newPuzzleLevelStarted = _newPuzzleLevelStarted;
    }

    public void SetRestartLevel(bool _restartInvoked)
    {
        restartInvoked = _restartInvoked;
    }

}
