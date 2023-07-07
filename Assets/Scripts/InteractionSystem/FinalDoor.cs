using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt = "You are about to enter the final Challenge! \n" + "Press E to Open";
    [SerializeField] private string doorSceneName;
    [SerializeField] private InventorySystem inventory;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Player player;
    [SerializeField] private GameObject LoadingScreen;
    [SerializeField] private int requiredAmount;

    private bool canOpen = false;

    private int numberOfRustyKeys;
    private int numberOfGoldKeys;

    private SerializableDictionary<string, bool> _scenesCompleted;

    public string InteractionPrompt => _prompt;

    public bool Interact(Player interactor)
    {
        foreach (InventoryItem item in inventory.Container.Items)
        {
            if (item.ID == 2)
            {
                numberOfRustyKeys = item.StackSize;
            }
        }

        foreach (InventoryItem item in inventory.Container.Items)
        {
            if (item.ID == 0)
            {
                numberOfGoldKeys = item.StackSize;
            }
        }

        if (numberOfRustyKeys >= 4 || (numberOfRustyKeys >= 3 && numberOfGoldKeys >= 1))
        {
            var items = inventory.Container.Items.Find(x => x.ID == 1);
            var nroOfItems = 0;
            if (items != null)
            {
                Debug.Log(items.StackSize);
                nroOfItems = items.StackSize;
            }
            if (nroOfItems < requiredAmount)
            {
                string secondaryPrompt = $"Find {requiredAmount - nroOfItems} diary pages to open.";
                if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
                interactionPromptUI.SetUp(secondaryPrompt);
                return false;
            }
            else
            {
                canOpen = true;
            }
        }
        else
        {
            canOpen = false;
        }

        if (canOpen)
        {
            Debug.Log("Opening Door!");
            DataPersistenceManager.instance.SetNewLevel(true);
            DataPersistenceManager.instance.SaveGame();
            //SceneManager.LoadSceneAsync(doorSceneName);
            LoadScene();
            SetPlayerPosition();
            DataPersistenceManager.instance.SaveGame();
            return true;
        }
        else
        {
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
            interactionPromptUI.SetUp($"You need 4 rusty keys or 1 golden and 3 rusty keys for this door!");
            return false;
        }

        Debug.Log("No key found!");
        return false;
    }

    public void SetPlayerPosition()
    {
        player.transform.position = new Vector3(-5.69999981f, 0.25000006f, -13.4399996f);
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
