using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Timeline;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private void Start()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerDataManager.gameRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                PlayerDataManager.gameRunning = false;
                PlayerDataManager.rounds++;
                PlayerDataManager.ChangeLevels();
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
    }
}
