using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class pauseMenu : MonoBehaviour
{
    SpecialControls controls;

    public static bool isPaused =  false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    public GameObject pauseMenuFirstButton;
    public GameObject optionsMenuFirstButton;

    private void Awake()
    {
        controls = new SpecialControls();

        controls.controls.pause.performed += ctx => pauseActivate();
    }

    void OnEnable()
    {
        controls.controls.Enable();
    }

    void OnDisable()
    {
        controls.controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActivate();
        }
    }

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
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }

    public void openOptions()
    {
        // Hide the pause menu and show the options menu
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    public void closeOptions()
    {
        // Hide the options menu and return to the pause menu
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }

    public void quit()
    {
        SceneManager.LoadScene(0);
        resume();
    }
}
