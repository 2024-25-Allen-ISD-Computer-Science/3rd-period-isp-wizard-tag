using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class pauseMenu : MonoBehaviour
{

    //This chunk includes all of the inports, booleans, and game objects that are required to make thi script run.
    SpecialControls controls;

    public static bool isPaused =  false;

    public bool mouseAct = false;
    public bool controllerAct = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public GameObject pauseMenuFirstButton;
    public GameObject optionsMenuFirstButton;


    //Awake sets the controller map to "controls", and assigns each action to a function. 
    private void Awake()
    {
        controls = new SpecialControls();

        controls.controls.pause.performed += ctx => pauseActivate();
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
        if (controllerAct == false && isPaused == true)
        {
            if (pauseMenuUI.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
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
        }
        
    }


    //if the mouse becomes acive in the menu, then it will just allow the mouse to click. 
    void setMouseActive()
    {
        if (mouseAct == false && isPaused == true)
        {
            EventSystem.current.SetSelectedGameObject(null);
            controllerAct = false;
            mouseAct = true;

        }
    }

    // Update is called once per frame and detects if the escape key or mouse button is clicked.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActivate();
        }

        if (Input.GetMouseButtonDown(0))
        {
            setMouseActive();
        }
    }

    //If the escape key is clicked or the start button is pressed, then this activated paused mode
    private void pauseActivate()
    {

        Debug.Log("pause.");
        if (isPaused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }

    //This is the script that resumes the game: setting time back on, and closing the menus.
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    //this is the script that pauses the game: opening the menus, and stopping time
    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }

    //This is for when the options button is clicked: it opens the options menu and closes the pause menu
    //it sets the active button the first options button.
    public void openOptions()
    {
        // Hide the pause menu and show the options menu
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    //this is for when the options menu is closes: it opens the pause menu and closes the options
    //sets the first main menu button active
    public void closeOptions()
    {
        // Hide the options menu and return to the pause menu
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }

    //This is the quit button of the pause menu
    //it changes the scene to the main menu and closes the game. 
    public void quit()
    {
        SceneManager.LoadScene(0);
        resume();
    }
}
