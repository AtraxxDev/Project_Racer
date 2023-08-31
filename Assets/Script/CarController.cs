using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphererb;

    public float forwardAccel = 8f,reverseAccel = 4f,maxSpeed = 50f, turnStrenght = 100 , gravityForce = 10f, dragOnGround = 3f;
    
    private float speedInput, turnInput;

    private bool grounded;

    public float driftStrength = 20f; // Ajusta la fuerza de derrape.
    private bool isDrifting = false;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;


    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    void Start()
    {
        sphererb.transform.parent = null;
    }

    void Update()
    {
        speedInput = 0f;
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrenght * Time.deltaTime * Input.GetAxis("Vertical"), 0f));

        }

        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);


        transform.position = sphererb.transform.position;


        // Verifica si se presiona la tecla de espacio para iniciar el derrape.
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            isDrifting = true;
        }

        // Cuando se libera la tecla de espacio, deja de derrapar.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isDrifting = false;
        }

        // Si estamos derrapando, aplica la fuerza de derrape.
        if (isDrifting)
        {
            // Ajusta la rotación del coche para dar la sensación de derrape.
            float rotationAngle = turnInput * maxWheelTurn * 2f; // Aumenta el factor multiplicador según lo necesario.
            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);

            // Aplica una fuerza lateral para el derrape.
            Vector3 driftForce = transform.right * (rotationAngle * driftStrength);
            sphererb.AddForce(driftForce);
        }

    }

    private void FixedUpdate()
    {

        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength , whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded)
        {
            sphererb.drag = dragOnGround;
           if(Mathf.Abs(speedInput) > 0) 
           {
             sphererb.AddForce(transform.forward * speedInput);
           }

        }
        else
        {
            sphererb.drag = 0.1f;
           sphererb.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }
}
