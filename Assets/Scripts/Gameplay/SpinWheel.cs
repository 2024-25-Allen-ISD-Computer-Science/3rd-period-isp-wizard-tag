using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

using Color = UnityEngine.Color;
using UnityEditor.ShaderGraph;

public class SpinWheel : MonoBehaviour
{
    private SpecialControls controls;

    public GameObject Pointer;
    public GameObject TwoPWheel;
    public GameObject ThreePWheel;
    public GameObject FourPWheel;
    private GameObject activeWheel;

    [SerializeField] private GameObject[] RedColliders; //There should be 3 Red Colliders
    [SerializeField] private GameObject[] BlueColliders; //3 Blue Colliders
    [SerializeField] private GameObject[] GreenColliders; //2 Green Collider
    public GameObject yellowCollider;
    public GameObject backgroundDarken;

    private int playerCount = PlayerDataManager.AssignedPlayers.Count;
    public static bool playersCanMove = false;

    public static bool isSpinning = false;
    public float spinSpd;
    public float maxSpd = 500f;
    public float slowDownSpd = 2f;

    private bool spinTriggered = false;



    private void Start()
    {
        Pointer.SetActive(false);
        TwoPWheel.SetActive(false);
        ThreePWheel.SetActive(false);
        FourPWheel.SetActive(false);

        if (playerCount != 1) {
            startMatch();
        }
        else
        {
            playersCanMove = true;
        }
    }
    public void startMatch()
    {
        playersCanMove = false;

        backgroundDarken.SetActive(playerCount > 1);
        Pointer.SetActive(playerCount > 1);
        if (playerCount == 2)
        {
            TwoPWheel.SetActive(true);
            activeWheel = TwoPWheel;
        }
        else if (playerCount == 3)
        {
            ThreePWheel.SetActive(true);
            activeWheel = ThreePWheel;
        }
        else if (playerCount == 4)
        {
            FourPWheel.SetActive(true);
            activeWheel = FourPWheel;
        }
        else
        {
            activeWheel = null;
        }

        StartCoroutine(WaitForSpinInput());
    }

    private IEnumerator WaitForSpinInput()
    {
        spinTriggered = false;
        Debug.Log("Press A to spin the wheel...");

        while (!spinTriggered)
        {
            foreach (Gamepad gamepad in Gamepad.all)
            {
                if (gamepad.buttonSouth.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame) // "A" Button
                {
                    spinTriggered = true;
                    spin();
                    break;
                }
            }
            yield return null;
        }
    }

    private void spin()
    {
        spinSpd = Random.Range(maxSpd * 0.5f, maxSpd * 1.2f);
        isSpinning = true;
        StartCoroutine(WheelSpin());
    }

    private IEnumerator WheelSpin()
    {
        while (spinSpd > 0)
        {
            activeWheel.transform.Rotate(Vector3.forward * spinSpd * Time.deltaTime);
            spinSpd -= slowDownSpd * Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
        determinePlayer();
    }

    private void determinePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(Pointer.transform.position, Vector2.down);
        if (hit.collider != null)
        {
            string hitPoint = hit.collider.tag;

            if (hitPoint == "Red")
            {
                Debug.Log("Player 0 is it!");
                PlayerDataManager.taggedPlayer = 0;

            }
            else if (hitPoint == "Blue")
            {
                Debug.Log("Player 1 is it!");
                PlayerDataManager.taggedPlayer = 1;
            }
            else if (hitPoint == "Green")
            {
                Debug.Log("Player 2 is it!");
                PlayerDataManager.taggedPlayer = 2;
            }
            else if (hitPoint == "Yellow")
            {
                Debug.Log("Player 3 is it!");
                PlayerDataManager.taggedPlayer = 3;
            }
            else
            {
                Debug.Log("Defaulting to P0");
                PlayerDataManager.taggedPlayer = 0;
            }
        }

        StartCoroutine(FadeOut());

        playersCanMove = true;
    }




    private IEnumerator FadeOut()
    {
        Debug.Log("FadeOut started");
        float duration = 1f;
        float elapsedTime = 0f;

        SpriteRenderer pointerRenderer = Pointer.GetComponent<SpriteRenderer>();
        SpriteRenderer Wheel1Renderer = TwoPWheel.GetComponent<SpriteRenderer>();
        SpriteRenderer Wheel2Renderer = ThreePWheel.GetComponent<SpriteRenderer>();
        SpriteRenderer Wheel3Renderer = FourPWheel.GetComponent<SpriteRenderer>();
        SpriteRenderer BlackBoxRenderer = backgroundDarken.GetComponent<SpriteRenderer>();

        Color PstartColor = pointerRenderer.color;
        Color w1Color = Wheel1Renderer.color;
        Color w2Color = Wheel2Renderer.color;
        Color w3Color = Wheel3Renderer.color;
        Color bbColor = BlackBoxRenderer.color;

        float startbbAlpha = BlackBoxRenderer.color.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float transparency = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            pointerRenderer.color = new Color(PstartColor.r, PstartColor.g, PstartColor.b, transparency);
            Wheel1Renderer.color = new Color(w1Color.r, w1Color.g, w1Color.b, transparency);
            Wheel2Renderer.color = new Color(w2Color.r, w2Color.g, w2Color.b, transparency);
            Wheel3Renderer.color = new Color(w3Color.r, w3Color.g, w3Color.b, transparency);
            //BlackBoxRenderer.color = new Color(bbColor.r, bbColor.g, bbColor.b, transparency);

            float bbTransparency = Mathf.Lerp(startbbAlpha, 0f, elapsedTime / duration);
            BlackBoxRenderer.color = new Color(BlackBoxRenderer.color.r, BlackBoxRenderer.color.g, BlackBoxRenderer.color.b, bbTransparency);

            yield return null;
        }

        Pointer.SetActive(false); // Optionally disable it
        TwoPWheel.SetActive(false);
        ThreePWheel.SetActive(false);
        FourPWheel.SetActive(false);
        backgroundDarken.SetActive(false);

        PlayerDataManager.gameRunning = true;
    }


}

