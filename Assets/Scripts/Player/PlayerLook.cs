using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] PlayerMovement pm;
    public static Action<bool> aimingInput;
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
        aimingInput?.Invoke(true);
        pm.setMovSpeed(3.5f);
        Vector3 pointToLook = GetPointToLook();
        transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

      }else if(Input.GetMouseButtonUp(1)){

        aimingInput?.Invoke(false);
        pm.setMovSpeed(7f); 
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
