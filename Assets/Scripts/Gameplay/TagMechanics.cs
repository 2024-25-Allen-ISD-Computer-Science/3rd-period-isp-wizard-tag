using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TagMechanics : MonoBehaviour
{
    public bool isTagged = false;
    public int playerIndex;
    public float tageTimeBuffer = 5f;
    private bool canTag = true;

    private int otherIndex = 5;

    public GameObject taggedPointer;

    // Start is called before the first frame update
    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerIndex = playerInput.playerIndex; // Uses Unity's auto-assigned index
        taggedPointer.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //if the playerDataManager equals this players index, they are tagged

        isTagged = (PlayerDataManager.taggedPlayer == playerIndex);

        taggedPointer.SetActive(isTagged);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Players are touching");
            var otherPlayer = other.GetComponent<TagMechanics>();

            if (isTagged != otherPlayer.isTagged)
            {
                canTag = false;
                otherPlayer.canTag = false;

                StartCoroutine(canTagAfterDelay());

                isTagged = false;
                PlayerDataManager.taggedPlayer = otherPlayer.playerIndex;
                Debug.Log($"{other.gameObject.name} is now tagged!");
            }

        }
    }

    private IEnumerator canTagAfterDelay()
    {
        yield return new WaitForSeconds(tageTimeBuffer);
        canTag = true; // Enable tagging
    }

}
