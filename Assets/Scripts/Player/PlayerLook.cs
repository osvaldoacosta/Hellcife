
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerLook : MonoBehaviour
{
    private Camera mainCamera;
    public static Action<bool> aimingInput;
    [SerializeField] PlayerMovement playerMovement;
    void Start()
    {
        this.mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMovement();
    }
    private void MouseMovement()
    {
      if(Input.GetMouseButton(1)){
        playerMovement.isAiming= true;
        aimingInput?.Invoke(true);
        Vector3 pointToLook = GetPointToLook();
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
      }else if(Input.GetMouseButtonUp(1)){
        aimingInput?.Invoke(false);
        playerMovement.isAiming= false;
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
