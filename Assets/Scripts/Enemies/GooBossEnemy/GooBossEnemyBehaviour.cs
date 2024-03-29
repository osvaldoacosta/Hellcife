using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class GooBossEnemyBehaviour : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;
    public GameObject target;

    //ranges
    public float selfPuddleAttackRange= 5f;
    public float attackRange= 9f;
    
    //cooldown times
    public float attackFinishingDuration= 2.0f;
    public float attackWindupDuration= 0.8f;
    public float attackCooldownDuration= 5.0f;

    //end of cooldowns
    private float endOfAttackWindup = -1f;
    private float endOfAttack= -1f;
    private float endOfAttackCooldown= -1f;

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
    [SerializeField] GooBossProjectileShooter gooProjectileShooter;
    [SerializeField] public GameObject gooPuddleObjectPool;
    
    //The "goo" attack is shot in a parabola and its parameters can me modified here
    //parabolaMaxHeight is purely aesthetic
    float parabolaMaxHeight= 7f;

    public int numberOfProjectilesPerAttack = 5;
    public float gooShotImprecision= 5f;
    public float gooProjectileAirTime= 1.5f;
    public float gooPuddleRadius = 5f;
    public float gooPuddleDuration = 5f;
    public float selfGooPuddleRadius = 8f;
    public float selfGooPuddleDuration= 6f;
    
    private float runAnimationOffset;
    private Animator animator;

    void Start(){
        runAnimationOffset= Random.Range(0, 1f);
        animator = GetComponent<Animator>();
        animator.SetFloat("RunOffset", runAnimationOffset);
        target= GameObject.FindWithTag("Player");
        gooPuddleObjectPool= GameObject.FindWithTag("GooPuddlePool");
    }
    // Update is called once per frame
    void Update()
    {
        animateAction();
        if(isActionLocked()){
            switch (activeEnemyState){
                case EnemyStates.windingUpAttack:
                    if(Time.time< endOfAttackWindup){
                        return;
                    }
                    gooAttack();
                    activeEnemyState= EnemyStates.finishingAttack;
                    return;
                case EnemyStates.finishingAttack:
                    if( Time.time < endOfAttack ){
                        //silly attack animation for debug purposes
                        transform.localScale= new Vector3(1.6f, 2.4f, 1.6f);
                        return;
                    }
                    //silly attack animation for debug purposes
                    transform.localScale= new Vector3(2f, 2f, 2f);
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
    private void createGooPuddle(Vector3 gooPuddleCoord, float gooPuddleRadius, float gooPuddleDuration){
        GameObject newGooPuddle= gooPuddleObjectPool.GetComponent<ObjectPool>().GetPooledObject(); 
        if (newGooPuddle != null) {
            newGooPuddle.transform.position = gooPuddleCoord;
            newGooPuddle.transform.rotation = transform.rotation;
            newGooPuddle.GetComponent<GooPuddleBehaviour>().setGooPuddle(gooPuddleDuration, gooPuddleRadius);
            newGooPuddle.SetActive(true);
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
    private bool isTargetInAttackRange(){
        return (horizontalDistanceFromTargetVector.magnitude < attackRange);
    }
    private bool isTargetInSelfPuddleAttackRange(){
        return (horizontalDistanceFromTargetVector.magnitude < selfPuddleAttackRange);
    }
    private void beIdle(){
        //stop in place
        enemyNavMeshAgent.SetDestination(enemyCoord);
    }
    private void followTarget(){
        enemyNavMeshAgent.SetDestination(targetCoord);
    }
    private void startAttackWindup(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        //silly attack animation for debug purposes
        transform.localScale = new Vector3(2.6f, 1.6f, 2.6f);
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
                if(isTargetInAttackRange()){
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
            case EnemyStates.windingUpAttack:
                startAttackWindup();
                break;
        }
    }
    private void gooAttack(){
        //silly attack animation for debug purposes
        transform.localScale= new Vector3(2f, 2f, 2f);
        //make enemy stop
        enemyNavMeshAgent.SetDestination(enemyCoord);
        acquireSelfCoordsAndTargetCoords();

        if(isTargetInSelfPuddleAttackRange()){
            createGooPuddle(enemyCoord, selfGooPuddleRadius, selfGooPuddleDuration);
            endOfAttack= Time.time + attackFinishingDuration;
            endOfAttackCooldown= Time.time + attackCooldownDuration;
            return;
        }

        Vector3 launchPoint = gooProjectileShooter.transform.position;
        Vector3 gooProjectileLandingCoord = new Vector3(targetCoord.x, targetCoord.y, targetCoord.z);
        List<Vector3> allShotsCoords= new List<Vector3>();
        for(int i=0; i< numberOfProjectilesPerAttack; i++){
            allShotsCoords.Add(addRandomnessToXZCoords(gooProjectileLandingCoord, gooShotImprecision));
        }
        foreach (Vector3 gooShotCoords in allShotsCoords)
        {
            //create the projectile and send it
            gooProjectileShooter.launchGooProjectile(launchPoint, parabolaMaxHeight, gooShotCoords, gooProjectileAirTime, gooPuddleRadius, gooPuddleDuration);
        }
        
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

