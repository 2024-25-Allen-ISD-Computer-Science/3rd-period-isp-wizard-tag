using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class mainMenu : MonoBehaviour
{
    SpecialControls controls;

    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;
    public GameObject levelSelectMenuUI;

    public GameObject mainMenuFirstButton;
    public GameObject optionsMenuFirstButton;
    public GameObject levelSelectMenuFirstButton;

    public bool mouseAct = false;
    public bool controllerAct = false;

    private PlayerInput playerInput; // Reference to PlayerInput component
    private string assignedActionMap; // Assigned action map for this player

    private void Awake()
    {
        controls = new SpecialControls();
        controls.controls.navigate.performed += ctx => setControllerActive();
        controls.controls.click.performed += ctx => setMouseActive();
    }

    //OnEnable enables the controller when it is used. 
    void OnEnable()
    {
        controls.controls.Enable();
    }

    //OnDisable disables the controller when another action type is found. 
    void OnDisable()
    {
        controls.controls.Disable();
    }

    //setControllerActive acrivates when navigation is detected from the controller
    //If the controller is not active, and it become active in a menu,
    //it will go to the first button of that menu.
    void setControllerActive()
    {
        if (controllerAct == false)
        {
            if (mainMenuUI.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                controllerAct = true;
                mouseAct = false;
            }
            else if (optionsMenuUI.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
                controllerAct = true;
                mouseAct = false;
            }
            else if (levelSelectMenuUI.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(levelSelectMenuFirstButton);
                controllerAct = true;
                mouseAct = false;
            }
        }

    }


    //if the mouse becomes acive in the menu, then it will just allow the mouse to click. 
    void setMouseActive()
    {
        if (mouseAct == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            controllerAct = false;
            mouseAct = true;

        }
    }

    private void Start()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(false);

        /*EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);*/
    }


    public void openLevelSelect()
    {
        mainMenuUI.SetActive(false);
        levelSelectMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        if (controllerAct == true)
        {
            EventSystem.current.SetSelectedGameObject(levelSelectMenuFirstButton);
        }
    }

    public void closeLevelSelect()
    {
        mainMenuUI.SetActive(true);
        levelSelectMenuUI.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        if (controllerAct == true)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }
    }

    public void openOptions()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        if (controllerAct == true)
        {
            EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
        }
    }

    public void closeOptions()
    {
        // Hide the options menu and return to the pause menu
        optionsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        if (controllerAct == true)
        {
            EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        }
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
