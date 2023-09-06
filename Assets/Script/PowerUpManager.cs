using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public List<PowerUpTimer> powerUpObjects;  // Lista de objetos de power-up y sus tiempos de reaparición individuales

    private void Start()
    {
        InitializePowerUps();
    }

    private void Update()
    {
        UpdatePowerUpTimers();
    }

    private void InitializePowerUps()
    {
        foreach (var powerUp in powerUpObjects)
        {
            if (powerUp.powerUpObject != null)
            {
                powerUp.powerUpObject.SetActive(true);  // Activa todos los objetos de power-up al inicio del juego
            }
        }
    }

    private void UpdatePowerUpTimers()
    {
        foreach (var powerUp in powerUpObjects)
        {
            if (powerUp.powerUpObject != null && !powerUp.powerUpObject.activeSelf)
            {
                powerUp.respawnTimer += Time.deltaTime;

                if (powerUp.respawnTimer >= powerUp.respawnTime)
                {
                    powerUp.powerUpObject.SetActive(true);
                    powerUp.respawnTimer = 0f;
                }
            }
        }
    }
}
