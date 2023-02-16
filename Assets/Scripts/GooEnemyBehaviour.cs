using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class GooEnemyBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;
    public GameObject target;

    //ranges
    public float panicRange= 7f;
    public float attackRange= 9f;
    public float panicRunDistance= 3f;
    
    //cooldown times
    public float panicDuration= 1.5f;
    public float attackDuration= 2.0f;
    public float attackCooldown= 5.0f;

    //end of cooldowns
    private float endOfAttack= -1f;
    private float endOfAttackCooldown= -1f;
    private float endOfPanic= -1f;

    //distances and vectors
    private Vector3 enemyCoord;
    private Vector3 targetCoord;
    private Vector3 horizontalDistanceFromTargetVector;
    private Vector3 panicRunDestination;

    //all possible enemy states
    public enum EnemyStates{
        idle = 0,
        following = 1,
        attacking = 2,
        panicking = 3
    }
    [SerializeField] private EnemyStates activeEnemyState;
    private bool ableToSeeSorroundings;

    //This enemy shoots projectiles, so there is a special handler
    [SerializeField] GooProjectileShooter gooProjectileShooter;
    
    //The "goo" attack is shot in a parabola and its parameters can me modified here
    float parabolaMaxHeight= 7f;
    public float shotImprecision= 3f;
    public float gooProjectileAirTime= 1.5f;
    public float gooPuddleRadius = 3f;
    public float gooPuddleDuration = 5f;

    // Update is called once per frame
    void Update()
    {
        if(isActionLocked()) return;
        acquireSelfCoordsAndTargetCoords();
        decideActiveState();
        enemyAction(activeEnemyState);
    }

    private bool isActionLocked(){
        if(activeEnemyState == EnemyStates.attacking){
            if( Time.time < endOfAttack ){
                return true;
            }
        }
        if(activeEnemyState == EnemyStates.panicking){
            if( Time.time < endOfPanic ){
                acquireSelfCoordsAndTargetCoords();
                calculateHorizontalDistanceFromTargetAsVector();
                calculatePanicRunDestination();
                enemyNavMeshAgent.SetDestination(panicRunDestination);
                return true;
            }
        }
        return false;
    }
    private void acquireSelfCoordsAndTargetCoords(){
        if(Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out var enemyHitInfo)){
            enemyCoord = enemyHitInfo.point;
            ableToSeeSorroundings= true;
        } else {
            ableToSeeSorroundings= false;
        }
        if(Physics.Raycast(target.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo)){
            targetCoord = hitInfo.point;
            ableToSeeSorroundings= true;
        } else {
            ableToSeeSorroundings= false;
        }
    }
    private void calculateHorizontalDistanceFromTargetAsVector(){
        horizontalDistanceFromTargetVector= enemyCoord - targetCoord;
        horizontalDistanceFromTargetVector.y= 0f;
    }
    private bool visionIsObstructed(){
        //ground raycast from the enemy to the target
        if(Physics.Raycast(enemyCoord, new Vector3(-horizontalDistanceFromTargetVector.x, 0f, -horizontalDistanceFromTargetVector.z), out var hitInfo, horizontalDistanceFromTargetVector.magnitude, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)){
            return true;
        } else {
            return false;
        }
    }
    private bool isTargetTooClose(){
        return (horizontalDistanceFromTargetVector.magnitude < panicRange);
    }
    private bool isTargetInAttackRange(){
        return (horizontalDistanceFromTargetVector.magnitude < attackRange);
    }
    private void calculatePanicRunDestination(){
        panicRunDestination= enemyCoord + (horizontalDistanceFromTargetVector.normalized * panicRunDistance);
    }
    private void beIdle(){
        //stop in place
        enemyNavMeshAgent.SetDestination(enemyCoord);
    }
    private void followTarget(){
        enemyNavMeshAgent.SetDestination(targetCoord);
    }
    private void fleeFromTarget(){
        calculatePanicRunDestination();
        enemyNavMeshAgent.SetDestination(panicRunDestination);
        endOfPanic = Time.time + panicDuration;
    }
    private void decideActiveState(){
        //Acquire all necessary cords and distances. If can't find the player or itself, stay idle
        if(!ableToSeeSorroundings){
            activeEnemyState= EnemyStates.idle;
        } else {
            calculateHorizontalDistanceFromTargetAsVector();
            //Check if the enemy vision to the target is obstructed
            if(visionIsObstructed()){
                activeEnemyState= EnemyStates.following;
            } else {
                if(isTargetTooClose()){
                    activeEnemyState= EnemyStates.panicking;
                } else if(isTargetInAttackRange()){
                    activeEnemyState= EnemyStates.attacking;
                } else {
                    activeEnemyState= EnemyStates.following;
                }
            }
        }
    }
    private void enemyAction(EnemyStates activeEnemyState){
        switch (activeEnemyState)
        {
            case EnemyStates.idle:
                beIdle();
                return;
            case EnemyStates.following:
                followTarget();
                break;
            case EnemyStates.panicking:
                fleeFromTarget();
                break;
            case EnemyStates.attacking:
                if(Time.time > endOfAttackCooldown){
                    gooAttack();
                }
                break;
            default:
                return;
        }
    }
    private void gooAttack(){
        //make enemy stop
        enemyNavMeshAgent.SetDestination(enemyCoord);

        /*TO-DO
        do an attack animation*/
        Vector3 launchPoint = gooProjectileShooter.transform.position;
        Vector3 gooProjectileLandingCoord = targetCoord;
        gooProjectileLandingCoord= addRandomnessToXZCoords(gooProjectileLandingCoord, shotImprecision); 

        //create the projectile and send it
        gooProjectileShooter.launchGooProjectile(launchPoint, parabolaMaxHeight, gooProjectileLandingCoord, gooProjectileAirTime, gooPuddleRadius, gooPuddleDuration);

        //update the attack animation cooldown and attack cooldown
        endOfAttack= Time.time + attackDuration;
        endOfAttackCooldown= Time.time + attackCooldown;
    }
    //add randomness to a xz vector in a circular pattern
    private Vector3 addRandomnessToXZCoords(Vector3 originalVector ,float imprecision){
        float missDistance= Random.Range(0, 1f) * imprecision;
        float missAngle = Random.Range(0, 360);
        Vector3 randomnessVector = new Vector3(0f, 0f, 0f);
        randomnessVector.x= Mathf.Cos(missAngle) * missDistance;
        randomnessVector.z= Mathf.Sin(missAngle) * missDistance;

        return (originalVector + randomnessVector);
    }
}

