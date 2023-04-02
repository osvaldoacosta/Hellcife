using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeBossEnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent enemyNavMeshAgent;
    public GameObject target;
    
    //TO-DO: ATTACK MECHANICS
    AttackHitbox attackHitbox;
    public float rushAttackRange= 10f;
    public float attackRange= 2f;
    public int attackDamage= 5;

    //attack durations
    public float attackFinishingDuration= 5.0f;
    public float attackWindupDuration= 0.3f;
    public float attackCooldownDuration= 1.0f;
    public float attackActiveTime= 0.1f;

    //end of cooldowns
    private float endOfAttackWindup = -1f;
    private float endOfAttack= -1f;
    private float endOfAttackCooldown= -1f;

    //distances and vectors
    private Vector3 enemyCoord;
    private Vector3 targetCoord;
    private Vector3 distanceFromTargetVector;

    //all possible enemy states
    public enum EnemyStates{
        idle = 0,
        following = 1,
        windingUpAttack = 2,
        finishingAttack = 3
    }

    [SerializeField] private EnemyStates activeEnemyState= 0;
    
    private bool ableToSeeSorroundings;

    // Update is called once per frame
    void Start(){
        enemyNavMeshAgent= GetComponent<NavMeshAgent>();
        target= GameObject.FindWithTag("Player");
        attackHitbox= GetComponentInChildren<AttackHitbox>(true);
    }
    void Update()
    {
        if(isActionLocked()){
            switch (activeEnemyState){
                case EnemyStates.windingUpAttack:
                    if(Time.time< endOfAttackWindup){
                        return;
                    }
                    meleeAttack();
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
        switch(activeEnemyState){
            default:
                if(!ableToSeeSorroundings){
                    activeEnemyState= EnemyStates.idle;
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
    private void startAttackWindup(){
        enemyNavMeshAgent.SetDestination(enemyCoord);
        //silly attack animation for debug purposes
        transform.localScale = new Vector3(2.6f, 1.6f, 2.6f);
        endOfAttackWindup= Time.time + attackWindupDuration;
    }
    private void calculateDistanceVector(){
        distanceFromTargetVector = enemyCoord - targetCoord;
        distanceFromTargetVector.y= 0f;
    }
    private void meleeAttack(){
        //silly attack animation for debug purposes
        transform.localScale= new Vector3(2f, 2f, 2f);
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
