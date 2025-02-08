using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpinWheel : MonoBehaviour
{
    public GameObject Pointer;
    public GameObject TwoPWheel;
    public GameObject ThreePWheel;
    public GameObject FourPWheel;

    [SerializeField] private GameObject[] RedColliders; //There should be 3 Red Colliders
    [SerializeField] private GameObject[] BlueColliders; //3 Blue Colliders
    [SerializeField] private GameObject[] GreenColliders; //2 Green Collider
    public GameObject yellowCollider;
    public GameObject backgroundDarken;

    private int playerCount = PlayerDataManager.AssignedGamepads.Count;
    public bool playersCanMove = false;


    private void Start()
    {
        Pointer.SetActive(false);
        TwoPWheel.SetActive(false);
        ThreePWheel.SetActive(false);
        FourPWheel.SetActive(false);

        startMatch();
    }
    public void startMatch()
    {
        backgroundDarken.SetActive(playerCount > 1);
        Pointer.SetActive(playerCount > 1);
        TwoPWheel.SetActive(playerCount == 2);
        ThreePWheel.SetActive(playerCount == 3);
        FourPWheel.SetActive(playerCount == 4);
    }
}
