using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRoll : MonoBehaviour
{
    public float rollForce = 500f;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }

    void Roll()
    {
        Vector3 rollDirection = transform.forward;
        rollDirection.y = 0f; // Ignore any vertical component
        rigidBody.AddForce(rollDirection.normalized * rollForce, ForceMode.Impulse);
    }
}