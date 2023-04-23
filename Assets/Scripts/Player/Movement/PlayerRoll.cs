using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerRoll : MonoBehaviour
{
    public float rollSpeed = 5f;
    
    private Rigidbody rigidBody;
    private Animator animator;
    private PlayerMovement pm;
    private PlayerRiggingModifier riggingModifier;
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator= GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        riggingModifier= GetComponent<PlayerRiggingModifier>();
    }

    void Update()
    {
        if(!animator.GetBool("isRolling"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("isRolling", true);
                riggingModifier.SetIdleRigWeight(0.0f);
                Debug.Log(rigidBody.velocity);
                StartCoroutine(Roll());
            }
        }
        
       
       

    }

    IEnumerator Roll()
    {
        pm.canMove = false;
        rigidBody.velocity = (transform.forward * rollSpeed/2);
        yield return new WaitForSeconds(0.15f);
        rigidBody.velocity = (transform.forward * rollSpeed);
        yield return new WaitForSeconds(0.60f);
        rigidBody.velocity = (transform.forward * (rollSpeed / 2f));
        yield return new WaitForSeconds(0.15f);


        rigidBody.velocity = (transform.forward * (rollSpeed / 3f));
        yield return new WaitForSeconds(0.15f);
        
        pm.canMove = true;
        animator.SetBool("isRolling", false);
        riggingModifier.SetIdleRigWeight(1.0f);
    }



}