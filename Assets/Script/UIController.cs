using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    public TextMeshProUGUI UITextCurrentLap;
    public TextMeshProUGUI UITextCurrentTime;
    public TextMeshProUGUI UITextLastLapTime;
    public TextMeshProUGUI UITextBestLapTime;

    public CheckPointsPlayer UpdateUIForPlayer;

    private int currentLap =-1;
    private float currentTime;
    private float lastLapTime;
    private float bestLapTime;

    // Update is called once per frame
    void Update()
    {
        if (UpdateUIForPlayer == null)
            return;

        if(UpdateUIForPlayer.CurrentLap!=currentLap)
        {
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"LAP: {currentLap}";
            
        }
        if (UpdateUIForPlayer.CurrentLapTime != currentTime)
        {
            currentTime = UpdateUIForPlayer.CurrentLapTime;
            UITextCurrentTime.text = $"TIME: {(int)currentTime / 60}:{(currentTime) % 60:00.00}";

        }
        if (UpdateUIForPlayer.CurrentLapTime != lastLapTime)
        {
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            UITextLastLapTime.text = $"LAST: {(int)lastLapTime / 60}:{(lastLapTime) % 60:00.00}";

        }
        if (UpdateUIForPlayer.BestLapTime != bestLapTime)
        {
            bestLapTime = UpdateUIForPlayer.BestLapTime;
            UITextBestLapTime.text = bestLapTime<100000000 ? $"BEST: {(int)bestLapTime / 60}:{(bestLapTime) % 60:00.00}": "BEST: NONE";

        }
    }
}
