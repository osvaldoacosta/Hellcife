using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGooPuddleInteraction : MonoBehaviour
{
    [SerializeField] float puddleDmgPerSecond;
    private float timeSinceLastDamage;
    private PlayerMovement playerMovement;
    private void Start()
    {
        timeSinceLastDamage = 0f;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }
    

    // Update is called once per frame
    void Update()
    {
        timeSinceLastDamage += Time.deltaTime;
       
        if (Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out var hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide)){
            if(hitInfo.collider.gameObject.tag == "GooPuddle"){
                playerMovement.isOnGooPuddle= true;
                if(timeSinceLastDamage >= 1)
                {
                    
                    gameObject.GetComponent<Target>().TakeDamage(puddleDmgPerSecond);
                    timeSinceLastDamage = 0f;
                }
                
                return;
            }
            playerMovement.isOnGooPuddle= false;
        }
        playerMovement.isOnGooPuddle= false;
    }
}
