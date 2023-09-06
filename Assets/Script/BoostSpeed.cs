using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpeed : MonoBehaviour
{
    public float boostAmount = 10f;  // Cantidad de aumento de velocidad.
    public float duration = 3f;      // Duración del power-up.
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que solo el jugador pueda recoger el power-up.
        {
            
            
            Debug.Log("Estoy colisionando");
            // Busca el objeto con el CarController por su nombre.
            GameObject carObject = GameObject.Find("Car");

            // Verifica si el objeto se encontró correctamente.
            if (carObject != null)
            {
                // Intenta obtener el componente CarController del objeto encontrado.
                CarController carController = carObject.GetComponent<CarController>();

                // Verifica si se encontró el componente CarController.
                if (carController != null)
                {
                    // Aplica el power-up al CarController.
                    carController.ApplySpeedBoost(boostAmount, duration);
                    Debug.Log("Estoy en boost");
                    carController.nitroParticles1.Play();
                    carController.nitroParticles2.Play();
                }
                else
                {
                    Debug.LogError("No se encontró el componente CarController en el objeto.");
                }
            }
            else
            {
                Debug.LogError("No se encontró el objeto con el nombre especificado.");
            }

            gameObject.SetActive(false); // Desactiva el objeto del power-up.
        }
    }

}
