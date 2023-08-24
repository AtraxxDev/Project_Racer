using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphererb;
    public float forwardAccel = 8f,reverseAccel = 4f,maxSpeed = 50f, turnStrenght = 100;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = sphererb.transform.position;
    }

    private void FixedUpdate()
    {
        sphererb.AddForce(transform.forward * forwardAccel);
    }
}
