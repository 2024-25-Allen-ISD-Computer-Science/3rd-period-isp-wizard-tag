using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoinPlayerMenu : MonoBehaviour
{
    private SpecialControls controls;

    [SerializeField] private GameObject[] playerPanels; // Assign your 4 player panels in the Inspector
    [SerializeField] private Color[] playerColors = { Color.red, Color.yellow, Color.green, Color.blue }; // Colors for each panel
    [SerializeField] private Color inactiveColor = Color.gray;

    public Button playButton;
    public Button backButton;

    public bool[] playerJoined = new bool[4]; // Track whether a player has joined
    private Gamepad[] assignedGamepads = new Gamepad[4];
    public bool inputEnabled = false;
    private bool KBPlayer1Assigned = false;
    private bool KBPlayer2Assigned = false;

    private IEnumerator EnableInputAfterDelay()
    {
        yield return new WaitForSeconds(.05f);
        inputEnabled = true; // Enable input detection
    }

    private IEnumerator WaitToResetPlayers()
    {
        yield return new WaitForSeconds(0.1f);
        ResetPanels();
    }

    private void Start()
    {
        // Initialize all panels to gray
        for (int i = 0; i < playerPanels.Length; i++)
        {
            var panelImage = playerPanels[i].GetComponent<Image>();
            panelImage.color = inactiveColor;
        }

        ResetPanels();
        playButton.interactable = false;
        backButton.interactable = false;

        /*var eventSystem = UnityEngine.EventSystems.EventSystem.current;
        if (eventSystem != null) eventSystem.sendNavigationEvents = false;*/


    }

    private void Update()
    {
        if (!inputEnabled) return;

        // Check each connected gamepad
        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad.wasUpdatedThisFrame && gamepad.buttonSouth.wasPressedThisFrame) // "A" button on Xbox controllers
            {
                AssignPlayer(gamepad);
            }
        }

        // Detect Keyboard Joins
        if (!KBPlayer1Assigned && (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame))
        {
            AssignKBPlayer(1, Keyboard.current);
        }
        else if (!KBPlayer2Assigned && (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame))
        {
            AssignKBPlayer(2, Keyboard.current);
        }

        if (playerJoined[0] /*&& playerJoined[1]*/)
        {
            playButton.interactable = true;
            backButton.interactable = true;
        }
    }

    public void MMPlayButtonWait()
    {
        StartCoroutine(EnableInputAfterDelay());
        Debug.Log("Waiting to allow player control");
    }

    public void BackButtonReset()
    {
        playButton.interactable = false;
        backButton.interactable = false;
        inputEnabled = false;

        KBPlayer1Assigned = false;
        KBPlayer2Assigned = false;

        PlayerDataManager.ResetAssignedGamepads();

        Debug.Log("Players reset.");
        StartCoroutine(WaitToResetPlayers());
    }

    private void AssignPlayer(Gamepad gamepad)
    {
        for (int i = 0; i < assignedGamepads.Length; i++)
        {
            if (assignedGamepads[i] == gamepad)
            {
                Debug.Log($"Gamepad already assigned to Player {i + 1}");
                return; // Gamepad already assigned
            }
        }

        for (int i = 0; i < playerPanels.Length; i++)
        {

            if (!playerJoined[i]) // Check if the slot is available
            {
                playerJoined[i] = true;
                assignedGamepads[i] = gamepad; // Assign this gamepad to the player

                PlayerDataManager.AssignedPlayers.Add(gamepad);


                // Set the panel's color to the corresponding player color
                var panelImage = playerPanels[i].GetComponent<Image>();
                panelImage.color = playerColors[i];

                Debug.Log($"Player {i + 1} joined with {gamepad}");

                foreach (var gp in PlayerDataManager.AssignedPlayers)
                {
                    Debug.Log($"Assigned Gamepad: {gp}");
                }

                return;
            }
        }

        Debug.Log("All player slots are full.");
    }

    private void AssignKBPlayer(int playerNumber, Keyboard keyboard)
    {
        if ((playerNumber == 1 && KBPlayer1Assigned) || (playerNumber == 2 && KBPlayer2Assigned))
        {
            Debug.Log($"Keyboard slot {playerNumber} already assigned.");
            return;
        }

        for (int i = 0; i < playerPanels.Length; i++)
        {
            if (!playerJoined[i])
            {
                playerJoined[i] = true;
                playerPanels[i].GetComponent<Image>().color = playerColors[i];

                PlayerDataManager.AssignedPlayers.Insert(0, keyboard);


                if (playerNumber == 1) KBPlayer1Assigned = true;
                if (playerNumber == 2) KBPlayer2Assigned = true;

                Debug.Log($"Keyboard Player {playerNumber} joined as Player {i + 1}");
                return;
            }
        }

        Debug.Log("All player slots are full.");
    }

    private void ResetPanels()
    {
        // Reset all panels to inactive color and clear player states
        for (int i = 0; i < playerPanels.Length; i++)
        {
            var panelImage = playerPanels[i].GetComponent<Image>();
            panelImage.color = inactiveColor;
            playerJoined[i] = false;
            assignedGamepads[i] = null;
        }
    }

    public void ResetGame()
    {
        PlayerDataManager.AssignedPlayers.Clear();
        Debug.Log("Game reset. Player data cleared.");
        ResetPanels();
    }
}

