using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTimer : MonoBehaviour
{
    public GameObject powerUpObject;  // Objeto del power-up
    public float respawnTime = 10f;  // Tiempo de reaparición individual del power-up
    public float respawnTimer = 0f;  // Temporizador actual del power-up

    public PowerUpTimer(GameObject obj, float time)
    {
        powerUpObject = obj;
        respawnTime = time;
        respawnTimer = 0f;
    }
}
