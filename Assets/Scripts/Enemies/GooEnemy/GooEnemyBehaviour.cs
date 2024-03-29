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
    public float attackFinishingDuration= 2.0f;
    public float attackWindupDuration= 0.8f;
    public float attackCooldownDuration= 5.0f;

    //end of cooldowns
    private float endOfAttackWindup = -1f;
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
        panicking = 2,
        windingUpAttack = 3,
        finishingAttack = 4
    }
    //current enemy state + vision
    [SerializeField] private EnemyStates activeEnemyState= 0;
    private bool ableToSeeSorroundings;

    //This enemy shoots projectiles, so there is a special handler
    [SerializeField] GooProjectileShooter gooProjectileShooter;
    
    //The "goo" attack is shot in a parabola and its parameters can me modified here
    float parabolaMaxHeight= 7f;
    public float gooShotImprecision= 3f;
    public float gooProjectileAirTime= 1.5f;
    public float gooPuddleRadius = 3f;
    public float gooPuddleDuration = 5f;

    private float runAnimationOffset;
    private Animator animator;

    void Start(){
        runAnimationOffset= Random.Range(0, 1f);
        animator = GetComponent<Animator>();
        animator.SetFloat("RunOffset", runAnimationOffset);
        target= GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        animateAction();
        if(isActionLocked()){
            switch (activeEnemyState){
                case EnemyStates.panicking:
                    if( Time.time < endOfPanic ){
                        fleeFromTarget();
                        return;
                    }
                    break;
                case EnemyStates.windingUpAttack:
                    if(Time.time< endOfAttackWindup){
                        return;
                    }
                    gooAttack();
                    activeEnemyState= EnemyStates.finishingAttack;
                    return;
                case EnemyStates.finishingAttack:
                    if( Time.time < endOfAttack ){
                        return;
                    }
                    break;
                default:
                    break;
            }
        }
        acquireSelfCoordsAndTargetCoords();
        decideActiveState();
        enemyAction();
    }

    private bool isActionLocked(){
        switch (activeEnemyState){
            case EnemyStates.panicking:
            case EnemyStates.windingUpAttack:
            case EnemyStates.finishingAttack:
                return true;
            default:
                return false;
        }
    }
    private void acquireSelfCoordsAndTargetCoords(){
        if(Physics.Raycast(transform.position, new Vector3(0f, -1f, 0f), out var enemyHitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)){
            enemyCoord = enemyHitInfo.point;
            ableToSeeSorroundings= true;
        } else {
            ableToSeeSorroundings= false;
        }
        if(!target){
            ableToSeeSorroundings= false;
            return;
        }
        if(Physics.Raycast(target.transform.position, new Vector3(0f, -1f, 0f), out var hitInfo, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)){
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
        calculateHorizontalDistanceFromTargetAsVector();
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
        calculateHorizontalDistanceFromTargetAsVector();
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
        acquireSelfCoordsAndTargetCoords();
        calculatePanicRunDestination();
        enemyNavMeshAgent.SetDestination(panicRunDestination);
    }
    private void panic(){
        fleeFromTarget();
        endOfPanic = Time.time + panicDuration;
    }
    private void startAttackWindup(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        Vector3 enemyToTargetDirection= targetCoord-enemyCoord;
        transform.forward= new Vector3(enemyToTargetDirection.x, 0, enemyToTargetDirection.z);
        endOfAttackWindup= Time.time + attackWindupDuration;
    }

    private void decideActiveState(){
        if(!ableToSeeSorroundings){
            activeEnemyState= EnemyStates.idle;
            return;
        } else {
            //Check if the enemy vision to the target is obstructed
            if(visionIsObstructed()){
                activeEnemyState= EnemyStates.following;
                return;
            } else {
                if(isTargetTooClose()){
                    activeEnemyState= EnemyStates.panicking;
                    return;
                } else if(isTargetInAttackRange()){
                    if(Time.time < endOfAttackCooldown){
                        activeEnemyState= EnemyStates.idle;
                    } else {
                        activeEnemyState= EnemyStates.windingUpAttack;
                    }
                    return;
                } else {
                    activeEnemyState= EnemyStates.following;
                    return;
                }
            }
        }
        
    }
    private void animateAction(){
        animator.SetInteger("EnemyState", (int) activeEnemyState);
    }
    private void enemyAction(){
        switch (activeEnemyState){
            case EnemyStates.idle:
                beIdle();
                return;
            case EnemyStates.following:
                followTarget();
                break;
            case EnemyStates.panicking:
                panic();
                break;
            case EnemyStates.windingUpAttack:
                startAttackWindup();
                break;
        }
    }
    private void gooAttack(){
        //make enemy stop
        enemyNavMeshAgent.SetDestination(enemyCoord);

        acquireSelfCoordsAndTargetCoords();

        Vector3 launchPoint = gooProjectileShooter.transform.position;
        Vector3 gooProjectileLandingCoord = targetCoord;
        gooProjectileLandingCoord= addRandomnessToXZCoords(gooProjectileLandingCoord, gooShotImprecision); 

        //create the projectile and send it
        gooProjectileShooter.launchGooProjectile(launchPoint, parabolaMaxHeight, gooProjectileLandingCoord, gooProjectileAirTime, gooPuddleRadius, gooPuddleDuration);

        //update the attack animation cooldown and attack cooldown
        endOfAttack= Time.time + attackFinishingDuration;
        endOfAttackCooldown= Time.time + attackCooldownDuration;
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

