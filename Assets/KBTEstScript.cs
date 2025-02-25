using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardTest : MonoBehaviour
{

    private void Awake()
    {
        if (Keyboard.current != null)
        {
            Debug.Log("There is a keyboard!");
        }
    }
    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                Debug.Log("A key pressed");
            }
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                Debug.Log("D key pressed");
            }
        }
        else
        {
            //Debug.LogError("Keyboard is not detected.");
        }
    }
}
