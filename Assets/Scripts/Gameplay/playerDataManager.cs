using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class PlayerDataManager
{
    // A list to hold the gamepads of joined players
    public static List<Gamepad> AssignedGamepads = new List<Gamepad>();

    public static int taggedPlayer = -1; //have a nonexistent index be tagged first, so you can update it at the start of the match. 

    public static void ResetAssignedGamepads()
    {
        AssignedGamepads.Clear();
    }
}

