using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public static class PlayerDataManager
{
    // A list to hold the gamepads of joined players
    public static List<InputDevice> AssignedPlayers = new List<InputDevice>();

    public static int taggedPlayer = -1; //have a nonexistent index be tagged first, so you can update it at the start of the match. 

    public static void ResetAssignedGamepads()
    {
        AssignedPlayers.Clear();
    }

    public static void ChangeLevels()
    {
        if (rounds < 3)
        {
            RandomSceneLoader.LoadNextScene();
        }
        else
        {
            SceneManager.LoadScene(5);
        }
    }

    public static bool gameRunning = false;

    public static int rounds = 0;
}

