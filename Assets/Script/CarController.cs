using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody sphererb;
    private float originalForwardAccel;
    public float forwardAccel = 8f,reverseAccel = 4f, turnStrenght = 100 , gravityForce = 10f, dragOnGround = 3f;
    
    private float speedInput, turnInput;

    private bool grounded;

    public float driftStrength = 20f; // Ajusta la fuerza de derrape.
    private bool isDrifting = false;

    public LayerMask whatIsGround;
    public float groundRayLength = 0.5f;
    public Transform groundRayPoint;


    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTurn = 25f;

    // Power Up 
    [SerializeField]private bool isBoosting = false;
    [SerializeField]private bool isSlowed = false;
    private float boostTimer = 0f;
    private float slowedTimer = 0f;
    public ParticleSystem nitroParticles1;
    public ParticleSystem nitroParticles2;
    public ParticleSystem slowParticles1;
    public ParticleSystem slowParticles2;



    void Start()
    {
        sphererb.transform.parent = null;
        originalForwardAccel = forwardAccel;
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


        // Actualiza el temporizador del power-up.
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0)
            {
                // Restablece la velocidad original y desactiva el estado de aumento de velocidad.
                forwardAccel = originalForwardAccel;
                Debug.Log("Exit Boost");
                isBoosting = false;
                nitroParticles1.Stop();
                nitroParticles2.Stop();
            }
        }

        // Actualiza el temporizador del power-up.
        if (isSlowed)
        {
            slowedTimer -= Time.deltaTime;
            if (slowedTimer <= 0)
            {
                // Restablece la velocidad original y desactiva el estado de aumento de velocidad.
                forwardAccel = originalForwardAccel;
                Debug.Log("Exit slowed");
                isSlowed = false;
                slowParticles1.Stop();
                slowParticles2.Stop();
                
            }
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


    public void ApplySpeedBoost(float amount, float duration)
    {
        isBoosting = true;
        boostTimer = duration;
        // Aplica el aumento de velocidad.
        forwardAccel += amount;
    }


    public void ApplySpeedSlowed(float amount, float duration)
    {
        isSlowed = true;
        slowedTimer = duration;
        // Aplica el aumento de velocidad.
        forwardAccel += amount;
    }

}
