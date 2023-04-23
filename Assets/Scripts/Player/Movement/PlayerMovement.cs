using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Math;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float defaultMovementSpeed;
    public float movementSpeed;
    public bool isOnGooPuddle = false;
    public float gooPuddleMovementSpeedDebuff = 2f;
    public bool isAiming = false;
    public float aimingMovementSpeedDebuff = 1f;
    public bool isRunning = false;
    public bool canMove = true;
    private Vector3 movementDirection;
    private PlayerRiggingModifier rigMod;
    public Vector3 GetMovDir() { return movementDirection; }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting player movement");
        rb = GetComponent<Rigidbody>();
        rigMod= GetComponent<PlayerRiggingModifier>();
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");



           

            //the total input is the hypotenuse of both vertical and horizontal.
            float totalInput = (float)Sqrt((Pow(horizontalInput, 2) + Pow(verticalInput, 2)));


            // Limiting the total input to be of the same magnitude (-1<=x<=1)
            if (totalInput > 1f)
            {
                horizontalInput = (horizontalInput / totalInput);
                verticalInput = (verticalInput / totalInput);
            }

            movementDirection = new Vector3(horizontalInput, 0f, verticalInput);



            isRunning = movementDirection != Vector3.zero;

                
            //Rotate player based on it's inputs
            if (movementDirection != Vector3.zero)
            {
                rotatePlayer(movementDirection);
            }

            //Appling aiming and goo puddle debuffs
            movementSpeed = defaultMovementSpeed;
            if (isOnGooPuddle)
            {
                movementSpeed -= gooPuddleMovementSpeedDebuff;
            }
            if (isAiming)
            {
                movementSpeed -= aimingMovementSpeedDebuff;
            }
            if (movementSpeed < 0f)
            {
                movementSpeed = 0f;
            }


        }

    }
    void FixedUpdate()
    {
        if (canMove)
        {
            movePlayer(movementDirection);
        }
        
    }
    private void rotatePlayer(Vector3 movementDirection)
    {
        float rotate_speed = 10f; //Mais implica numa rotacao mais rapida
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotate_speed); //Deixando a rotacao bem smoooooth com o slerp
    }





    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }
    private void movePlayer(Vector3 movementDirection)
    {

        rb.velocity = (movementDirection * movementSpeed);
    }
}

