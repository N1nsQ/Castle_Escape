using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private OptionsMenu optionsMenu;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private string currentSceneName;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.gameObject.SetActive(false);
            loadGameButton.gameObject.SetActive(false);
        }
    }

    // Getting current scene name
    public void LoadData(GameData data)
    {
        currentSceneName = data.currentScene;
    }

    // No need to save anything
    public void SaveData(GameData data)
    {
        //nothing
    }

    public void NewGame()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void LoadGame()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void Continue()
    {
        DisableMenuButtons();
        // Save the game anytime before a new scene
        DataPersistenceManager.instance.SaveGame();
        // Load the scene where player was when pressing esc - which will in turn load the game because of OnSceneLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(currentSceneName);

        Debug.Log("Continue from the last save");
    }

    public void Options()
    {
        optionsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void Quit()
    {
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("QUIT");
        Application.Quit();
    }

    // Disabled all the buttons, meant to be used first after click to prevent from double clicks
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        loadGameButton.interactable = false;
        quitButton.interactable = false;
        optionsButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
