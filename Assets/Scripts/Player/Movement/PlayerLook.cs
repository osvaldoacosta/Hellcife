
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerLook : MonoBehaviour
{
    private Camera mainCamera;
    public static Action<bool> aimingInput;
    [SerializeField] private PlayerMovement playerMovement;
    private PlayerRiggingModifier rigMod;
    private Animator animator;
    void Start()
    {
        this.mainCamera = Camera.main;
        rigMod= GetComponent<PlayerRiggingModifier>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }
    private void MouseMovement()
    {
      if (Input.GetMouseButton(1)){
        playerMovement.isAiming= true;
        aimingInput?.Invoke(true);
        Vector3 pointToLook = GetPointToLook();
        if(!animator.GetBool("isRolling")) //Previne que o player rode ao rolar
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        if (Input.GetMouseButtonDown(1)) //Previne perca de fps com o hold
        rigMod.OnAimStart();
      }else if(Input.GetMouseButtonUp(1)){
        aimingInput?.Invoke(false);
        playerMovement.isAiming= false;
        rigMod.OnAimEnd();
      }

    }

    public Vector3 GetPointToLook()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(ray, out rayLength))
        {
            return ray.GetPoint(rayLength);
        }
       return Vector3.zero;
    }
}
