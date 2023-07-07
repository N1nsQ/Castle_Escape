using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;

    public void OnBackClicked()
    {
        if (mainMenu != null) mainMenu.ActivateMenu();
        if (pauseMenu != null) pauseMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        // Set this menu to be active
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

}
