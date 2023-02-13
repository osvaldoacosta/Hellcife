using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 7f;
   
    public void setMovSpeed(float value){
      movementSpeed = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting player movement");
        this.rb = GetComponent<Rigidbody>();
        
        
    }


    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();
    }
    
    private void KeyboardMovement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontalInput * movementSpeed,rb.velocity.y,verticalInput * movementSpeed);
        
    }

    
}
