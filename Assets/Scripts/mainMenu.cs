using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{

    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject levelSelectMenuUI;

    public GameObject mainMenuFirstButton;
    public GameObject optionsMenuFirstButton;
    public GameObject levelSelectMenuFirstButton;

    private void Start()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }


    public void openLevelSelect()
    {
        mainMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(levelSelectMenuFirstButton);
    }

    public void closeLevelSelect()
    {
        mainMenuUI.SetActive(true);
        levelSelectMenuUI.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void openOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    public void closeOptions()
    {
        // Hide the options menu and return to the pause menu
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitButton ()
    {
        Debug.Log("quit.");
        Application.Quit ();
    }
}
