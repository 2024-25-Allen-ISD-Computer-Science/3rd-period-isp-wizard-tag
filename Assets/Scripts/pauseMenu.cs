using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class pauseMenu : MonoBehaviour
{
    SpecialControls controls;

    public static bool isPaused =  false;
    public GameObject pauseMenuUI;

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
        Time.timeScale = 1f;
        isPaused = false;
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void quit()
    {
        SceneManager.LoadScene(0);
    }
}
