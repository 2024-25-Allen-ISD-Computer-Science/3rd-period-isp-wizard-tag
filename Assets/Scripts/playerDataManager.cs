using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class PlayerDataManager
{
    // A list to hold the gamepads of joined players
    public static List<Gamepad> AssignedGamepads = new List<Gamepad>();

    public static void ResetAssignedGamepads()
    {
        AssignedGamepads.Clear();
    }
}

