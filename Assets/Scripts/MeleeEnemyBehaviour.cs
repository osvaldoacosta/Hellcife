using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        //Casts a ray downwards from the target and follows the point in which said ray hits the ground
        if(Physics.Raycast(target.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo)){
            enemyNavMeshAgent.SetDestination(hitInfo.point);
        }


    }
}
