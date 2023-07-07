using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class GoalDoor : MonoBehaviour, IInteractable, IDataPersistence
{
    [SerializeField] private string _prompt;
    [SerializeField] private string doorSceneName;
    [SerializeField] private int roomLevel;
    [SerializeField] private InventorySystem inventory;
    //[SerializeField] private int keyID;
    [SerializeField] private GameObject keyToFind;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Player player;
    //[SerializeField] private bool puzzleSolved;
    [SerializeField] private GameObject LoadingScreen;

    private string playedScene;

    public string InteractionPrompt => _prompt;

    public void SaveData(GameData data)
    {
        if (playedScene != null)
        {
            data.scenesCompleted.Add(playedScene, true);
        }
    }

    public void LoadData(GameData data)
    {
        //
    }

    public bool Interact(Player interactor)
    {
        if (this.gameObject.tag == "goal")
        {
            //Tää tarkastaa
            if (keyToFind == null)
            {
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    if (inventory.Container.Items.Any(x => x.ID == 10))
                    {
                        Debug.Log("Pass");

                        playedScene = SceneManager.GetActiveScene().name;
                        OnTimerToNull();
                        //SceneManager.LoadSceneAsync(doorSceneName);
                        LoadScene();

                        SetPlayerPosition(roomLevel);

                        DataPersistenceManager.instance.SaveGame();

                        return true;
                    }
                    else
                    {
                        if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                        interactionPromptUI.SetUp($"You need the Torch!");
                        return false;
                    }
                } else
                {
                    Debug.Log("Pass");

                    playedScene = SceneManager.GetActiveScene().name;
                    OnTimerToNull();
                    //SceneManager.LoadSceneAsync(doorSceneName);
                    LoadScene();

                    SetPlayerPosition(roomLevel);

                    DataPersistenceManager.instance.SaveGame();

                    return true;
                }
            }
            else
            {
                if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                interactionPromptUI.SetUp($"You need to find a key!");
            }
        }
        else if (gameObject.tag == "dectypt")
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp($"Am I Still a KING?");
        }
        else
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp($"You need to solve the puzzle!");
        }

        Debug.Log("Puzzle not solved yet!");
        return false;
    }

    public void SetPlayerPosition(int _roomLevel)
    {
        if (_roomLevel == 1)
        {
            player.transform.position = new Vector3(31, 0, -14);
            player.transform.rotation = new Quaternion(0, 0.718266487121582f, 0, -0.6957681179046631f);
        }
        if (_roomLevel == 2)
        {
            player.transform.position = new Vector3(-22.8608246f, 0.0377888083f, -11.7435255f);
            player.transform.rotation = new Quaternion(0, 0.904906988f, 0, 0.42560941f);
        }
        if (_roomLevel == 3)
        {
            //TÄHÄN VIIMEISEN SKENEN ALOITUSPOSITIO LOPULLISESSA VERSIOSSA
            player.transform.position = new Vector3(-3.59259224f, 10.0299997f, -5.42291737f);
            player.transform.rotation = new Quaternion(0, 0.890683353f, 0, -0.454624176f);
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(doorSceneName));
    }

    IEnumerator LoadSceneAsync(string doorSceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(doorSceneName);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
