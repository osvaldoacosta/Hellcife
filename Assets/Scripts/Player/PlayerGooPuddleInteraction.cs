using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGooPuddleInteraction : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out var hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide)){
            if(hitInfo.collider.gameObject.tag == "GooPuddle"){
                playerMovement.isOnGooPuddle= true;
                return;
            }
            playerMovement.isOnGooPuddle= false;
        }
        playerMovement.isOnGooPuddle= false;
    }
}
