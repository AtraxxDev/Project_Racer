using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSlickPowerUp : MonoBehaviour
{
    public float slowdownAmount = 0.5f;  // Cantidad de disminuci�n de velocidad.
    public float duration = 2f;          // Duraci�n de la disminuci�n de velocidad.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Aseg�rate de que solo el jugador pueda recoger el power-up.
        {
           

            // Busca el objeto con el CarController por su nombre.
            GameObject carObject = GameObject.Find("Car");

            // Verifica si el objeto se encontr� correctamente.
            if (carObject != null)
            {
                // Intenta obtener el componente CarController del objeto encontrado.
                CarController carController = carObject.GetComponent<CarController>();

                // Verifica si se encontr� el componente CarController.
                if (carController != null)
                {
                    // Aplica la disminuci�n de velocidad al CarController.
                    carController.ApplySpeedSlowed(-slowdownAmount, duration);
                    Debug.Log("Estoy en el charco de aceite");
                    carController.slowParticles1.Play();
                    carController.slowParticles2.Play();

                   
                    // Puedes agregar efectos adicionales aqu�, como part�culas de aceite o sonidos de derrape.
                }
                else
                {
                    Debug.LogError("No se encontr� el componente CarController en el objeto.");
                }
            }
            else
            {
                Debug.LogError("No se encontr� el objeto con el nombre especificado.");
            }

            gameObject.SetActive(false); // Desactiva el objeto del power-up.
        }
    }
}
