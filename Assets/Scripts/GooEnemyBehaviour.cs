using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooEnemyBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;
    public GameObject target;
    public float avoidanceRange= 7f;
    public float attackRange= 9f;
    public float panicDistance= 3f;

    Vector3 enemyCoord;
    Vector3 targetCoord;
    Vector3 distanceFromTargetVector;
    Vector3 avoidanceTarget;

    // Update is called once per frame
    void Update()
    {
        //Acquire all cords abd distances
        if(!acquireSelfCoordsAndTargetCoords()){
            return;
        }
        calculateDistanceVector();

        //Check if the enemy vision to the target is obstructed
        if(visionIsObstructed()){
            enemyNavMeshAgent.SetDestination(targetCoord);
            return;
        }
        
        //Decide what to do based on the distance form target
        if(isTargetTooClose()){
            calculateAvoidanceTarget();
            enemyNavMeshAgent.SetDestination(avoidanceTarget);
        } else if(isTargetInAttackRange()){
            gooAttack();
        } else {
            enemyNavMeshAgent.SetDestination(targetCoord);
        }
    }

    private bool acquireSelfCoordsAndTargetCoords(){
        if(Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out var enemyHitInfo)){
            enemyCoord = enemyHitInfo.point;
        } else {
            return false;
        }
        if(Physics.Raycast(target.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo)){
            targetCoord = hitInfo.point;
            return true;
        } else {
            return false;
        }
    }
    private void calculateDistanceVector(){
        distanceFromTargetVector = enemyCoord - targetCoord;
        distanceFromTargetVector.y= 0f;
    }
    private bool visionIsObstructed(){
        if(Physics.Raycast(enemyCoord, new Vector3(-distanceFromTargetVector.x, 0f, -distanceFromTargetVector.z), out var hitInfo, distanceFromTargetVector.magnitude)){
            return true;
        } else {
            return false;
        }
    }
    private bool isTargetTooClose(){
        return (distanceFromTargetVector.magnitude < avoidanceRange);
    }
    private bool isTargetInAttackRange(){
        return (distanceFromTargetVector.magnitude < attackRange);
    }
    private void calculateAvoidanceTarget(){
        avoidanceTarget = enemyCoord + (distanceFromTargetVector.normalized*panicDistance);
    }
    private void gooAttack(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        return;
    }
}

