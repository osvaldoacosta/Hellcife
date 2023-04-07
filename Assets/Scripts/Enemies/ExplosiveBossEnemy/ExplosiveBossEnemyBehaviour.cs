using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveBossEnemyBehaviour : MonoBehaviour
{
    [SerializeField] ObjectPool explosiveEnemyPool;
    [SerializeField] int numberOfMinionsPerSpawn;

    public NavMeshAgent enemyNavMeshAgent;
    public GameObject target;
    
    //TO-DO: ATTACK MECHANICS
    AttackHitbox attackHitbox;
    public float attackRange= 2f;
    public int attackDamage= 50;

    //attack durations
    public float attackFinishingDuration= 5.0f;
    public float attackWindupDuration= 0.3f;
    public float attackCooldownDuration= 1.0f;
    public float attackActiveTime= 0.1f;
    public float spawnMinionWindupDuration= 0.7f;
    public float spawnMinionsCooldownDuration= 10f;

    //end of cooldowns
    private float endOfAttackWindup = -1f;
    private float endOfAttack= -1f;
    private float endOfAttackCooldown= -1f;
    private float endOfSpawnMinionsWindup= -1f;
    private float endOfSpawnMinionsCooldown= -1f;

    //distances and vectors
    private Vector3 enemyCoord;
    private Vector3 targetCoord;
    private Vector3 distanceFromTargetVector;

    //all possible enemy states
    public enum EnemyStates{
        idle = 0,
        following = 1,
        windingUpAttack = 2,
        finishingAttack = 3,
        windingUpSpawnMinions = 4
    }

    [SerializeField] private EnemyStates activeEnemyState= 0;
    private Animator animator;
    private float runAnimationOffset;
    
    private bool ableToSeeSorroundings;

    // Update is called once per frame
    void Start(){
        animator = GetComponent<Animator>();
        runAnimationOffset= Random.Range(0, 1f);
        animator.SetFloat("RunOffset", runAnimationOffset);
        explosiveEnemyPool= GameObject.FindWithTag("ExplosiveEnemyObjectPool").GetComponent<ObjectPool>();
        enemyNavMeshAgent= GetComponent<NavMeshAgent>();
        target= GameObject.FindWithTag("Player");
        attackHitbox= GetComponentInChildren<AttackHitbox>(true);
    }
    void OnEnable(){
        activeEnemyState= EnemyStates.idle;
    }
    void Update()
    {
        
        if(isActionLocked()){
            switch (activeEnemyState){
                case EnemyStates.windingUpAttack:
                    if(Time.time< endOfAttackWindup){
                        return;
                    }
                    explosiveAttack();
                    activeEnemyState= EnemyStates.finishingAttack;
                    return;
                case EnemyStates.finishingAttack:
                    if( Time.time < endOfAttack ){
                        return;
                    }
                    break;
                case EnemyStates.windingUpSpawnMinions:
                    if(Time.time< endOfSpawnMinionsWindup){
                        return;
                    }
                    spawnMinions();
                    activeEnemyState= EnemyStates.idle;
                    return;
                default:
                    break;
            }
        }
        acquireSelfCoordsAndTargetCoords();
        decideActiveState();
        animateAction();
        enemyAction();
    }
    private bool isActionLocked(){
        switch (activeEnemyState){
            case EnemyStates.windingUpSpawnMinions:
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
    private void decideActiveState(){
        if(!ableToSeeSorroundings){
            activeEnemyState= EnemyStates.idle;
            return;
        } else {
            if(isTargetInAttackRange()){
                if(Time.time < endOfAttackCooldown){
                    if(Time.time < endOfSpawnMinionsCooldown){
                        activeEnemyState= EnemyStates.idle;
                        return;
                    }
                    activeEnemyState= EnemyStates.windingUpSpawnMinions;
                    return;
                } else {
                    activeEnemyState= EnemyStates.windingUpAttack;
                    return;
                }
            } else {
                if(Time.time < endOfSpawnMinionsCooldown){
                    activeEnemyState= EnemyStates.following;
                    return;
                }
                activeEnemyState= EnemyStates.windingUpSpawnMinions;
                return;
            }
        }
    }
    private void animateAction(){
        if((int) activeEnemyState == 4){
             animator.SetInteger("EnemyState", 2);
             return;
        }
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
            case EnemyStates.windingUpSpawnMinions:
                startSpawnMinionWindup();
                break;
            default:
                return;
        }
    }
    private void beIdle(){
        //stop in place
        enemyNavMeshAgent.SetDestination(enemyCoord);
    }
    private void followTarget(){
        enemyNavMeshAgent.SetDestination(targetCoord);
    }
    private void startSpawnMinionWindup(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        //silly attack animation for debug purposes
        endOfSpawnMinionsWindup= Time.time + spawnMinionWindupDuration;
    }
    private void startAttackWindup(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        //silly attack animation for debug purposes
        endOfAttackWindup= Time.time + attackWindupDuration;
    }
    private void calculateDistanceVector(){
        distanceFromTargetVector = enemyCoord - targetCoord;
        distanceFromTargetVector.y= 0f;
    }
    private void spawnMinions(){
        //silly attack animation for debug purposes
        for(int i=0; i<numberOfMinionsPerSpawn; i++){
            GameObject newMinion= explosiveEnemyPool.GetPooledObject();
            newMinion.transform.position= transform.position;
            newMinion.transform.position= new Vector3(newMinion.transform.position.x, 1.1f, newMinion.transform.position.z);
            newMinion.SetActive(true);
        }
        endOfSpawnMinionsCooldown= Time.time + spawnMinionsCooldownDuration;
    }
    private void explosiveAttack(){
        //make enemy stop
        enemyNavMeshAgent.SetDestination(enemyCoord);

        acquireSelfCoordsAndTargetCoords();

        attackHitbox.checkHitAndDealDamage(attackDamage, attackActiveTime);
        
        //update the attack animation cooldown and attack cooldown
        endOfAttack= Time.time + attackFinishingDuration;
        endOfAttackCooldown= Time.time + attackCooldownDuration;
    }
    private bool isTargetInAttackRange(){
        calculateDistanceVector();
        return (distanceFromTargetVector.magnitude < attackRange);
    }
}
