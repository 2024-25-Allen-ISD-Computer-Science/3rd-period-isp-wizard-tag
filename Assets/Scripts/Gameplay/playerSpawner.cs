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
        List<Gamepad> assignedGamepads = PlayerDataManager.AssignedGamepads;

        for (int i = 0; i < assignedGamepads.Count; i++)
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
                playerInput.SwitchCurrentControlScheme(assignedGamepads[i]);
                Debug.Log($"Player {i + 1} spawned with controller: {assignedGamepads[i]}");
            }
            else
            {
                Debug.LogError("Player prefab is missing a PlayerInput component!");
            }


            if (assignedGamepads[i] == null)
            {
                Debug.LogWarning($"No controller assigned for Player {i + 1}. Skipping spawn.");
                continue;
            }
        }
    }
}
