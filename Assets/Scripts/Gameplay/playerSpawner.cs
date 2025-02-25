using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab; // Assign your player prefab in the Inspector
    [SerializeField] private Transform[] spawnPoints; // Assign spawn points in the Inspector

    private void Start()
    {
        // Ensure we have at least one player prefab and spawn points
        if (playerPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("PlayerPrefab or SpawnPoints not set in PlayerSpawner!");
            return;
        }

        // Spawn players based on the saved controllers
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        List<InputDevice> assignedPlayers = PlayerDataManager.AssignedPlayers;

        for (int i = 0; i < assignedPlayers.Count; i++)
        {
            if (i >= spawnPoints.Length)
            {
                Debug.LogWarning("Not enough spawn points for all players!");
                break;
            }

            // Spawn the player at the corresponding spawn point
            GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);

            // Assign the controller to the PlayerInput component
            PlayerInput playerInput = playerInstance.GetComponent<PlayerInput>();

            if (playerInput != null)
            {
                // Assign control schemes correctly
                if (assignedPlayers[i] is Gamepad)
                {
                    playerInput.SwitchCurrentControlScheme("Gamepad", assignedPlayers[i]);
                }
                else if (assignedPlayers[i] is Keyboard)
                {
                    if (i == 0)
                    {
                        playerInput.SwitchCurrentControlScheme("KB1Mouse", assignedPlayers[i]);
                    }

                    else
                    {
                        playerInput.SwitchCurrentControlScheme("KB2Mouse", assignedPlayers[i]);
                    }
                }

                Debug.Log($"Player {i + 1} spawned with: {assignedPlayers[i]}");
            }
            else
            {
                Debug.LogError("Player prefab is missing a PlayerInput component!");
            }


            if (assignedPlayers[i] == null)
            {
                Debug.LogWarning($"No controller assigned for Player {i + 1}. Skipping spawn.");
                continue;
            }
        }
    }
}
