using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsPlayer : MonoBehaviour
{
    public float BestLapTime { get; private set; } = Mathf.Infinity;

    public float LastLapTime { get; private set; } = 0;

    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;

    private void Awake()
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoints");
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer!=checkpointLayer)
        {
            return;
        }
        //Si esto es el checkpoint 1
        if(collider.gameObject.name=="1")
        {
            //Y ya completamos toda una vuelva, entonces termina la vuelta de la pista
            if(lastCheckpointPassed==checkpointCount)
            {
                EndLap();
            }
            //Si estamos en la primera vuelta, o pasamos el último checkpoint, entonces empieza uno nuevo
            if(CurrentLap==0||lastCheckpointPassed==checkpointCount)
            {
                StartLap();
            }
            return;
        }

        //Si pasamos los siguientes checkpoint en orden, update el latestcheckpoint
        if(collider.gameObject.name==(lastCheckpointPassed+1).ToString())
        {
            lastCheckpointPassed++;
        }
    }
    private void StartLap()
    {
        Debug.Log("StartLAP!");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }


    void EndLap()
    {
       
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("EndLAP! - LapTime was "+LastLapTime +"seconds");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;
    }
}
