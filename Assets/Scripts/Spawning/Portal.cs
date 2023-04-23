using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<GroupSpawnData> groupsToSpawn= new List<GroupSpawnData>();
    public bool isSpawning= false;
    public GroupSpawnData currentGroup;
    public int currentWave= 0;

    float timeAtLastGroupSpawn= 0f;
    float spawnCooldown= 0f;

    [SerializeField] public EnemiesStats enemiesStats;
    [SerializeField] public ObjectPool gooEnemyPool;
    [SerializeField] public ObjectPool meleeEnemyPool;
    [SerializeField] public ObjectPool explosiveEnemyPool;
    [SerializeField] public ObjectPool gooBossEnemyPool;
    [SerializeField] public ObjectPool meleeBossEnemyPool;
    [SerializeField] public ObjectPool explosiveBossEnemyPool;

    public void Start(){
        GameEventManager.instance.onStartWave+= onStartWave;
        gooEnemyPool= GameObject.FindGameObjectWithTag("GooEnemyObjectPool").GetComponent<ObjectPool>();
        meleeEnemyPool= GameObject.FindGameObjectWithTag("MeleeEnemyObjectPool").GetComponent<ObjectPool>();
        explosiveEnemyPool= GameObject.FindGameObjectWithTag("ExplosiveEnemyObjectPool").GetComponent<ObjectPool>();
        gooBossEnemyPool= GameObject.FindGameObjectWithTag("GooBossEnemyPool").GetComponent<ObjectPool>();
        meleeBossEnemyPool= GameObject.FindGameObjectWithTag("MeleeBossEnemyPool").GetComponent<ObjectPool>();
        explosiveBossEnemyPool= GameObject.FindGameObjectWithTag("ExplosiveBossEnemyPool").GetComponent<ObjectPool>();
        enemiesStats = GameObject.FindGameObjectWithTag("EnemiesStatsObject").GetComponent<EnemiesStats>();
    }

    public void LateUpdate(){
        if(isSpawning){
            if(spawnCooldown + timeAtLastGroupSpawn > Time.time){
                return;
            }
            if(groupsToSpawn.Count == 0){
                isSpawning= false;
                return;
            }
            currentGroup= groupsToSpawn[0];
            spawnCooldown= groupsToSpawn[0].cooldownAfterThisSpawn;
            groupsToSpawn.RemoveAt(0);
            foreach(EnemySpawnData enemy in currentGroup.enemyGroup){
                GameObject newEnemy;
                switch(enemy.enemyType){
                    case "gooEnemy":
                        newEnemy = gooEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultGooEnemyHealth + enemiesStats.defaultGooEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultGooEnemySpeed + enemiesStats.defaultGooEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                    case "meleeEnemy":
                        newEnemy = meleeEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultMeleeEnemyHealth + enemiesStats.defaultMeleeEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultMeleeEnemySpeed + enemiesStats.defaultMeleeEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                    case "explosiveEnemy":
                        newEnemy = explosiveEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultExplosiveEnemyHealth + enemiesStats.defaultExplosiveEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultExplosiveEnemySpeed + enemiesStats.defaultExplosiveEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                    case "gooBossEnemy":
                        newEnemy = gooBossEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultGooBossEnemyHealth + enemiesStats.defaultGooBossEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultGooBossEnemySpeed + enemiesStats.defaultGooBossEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        GameEventManager.instance.bossSpawn();
                        break;
                    case "meleeBossEnemy":
                        newEnemy = meleeBossEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultMeleeBossEnemyHealth + enemiesStats.defaultMeleeBossEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultMeleeBossEnemySpeed + enemiesStats.defaultMeleeBossEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        GameEventManager.instance.bossSpawn();
                        break;
                    case "explosiveBossEnemy":
                        newEnemy = explosiveBossEnemyPool.GetPooledObject();
                        newEnemy.GetComponent<Target>().SetMaxHealth(enemiesStats.defaultExplosiveBossEnemyHealth + enemiesStats.defaultExplosiveBossEnemyHealth * ((float)(currentWave)*10f/100f));
                        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = (enemiesStats.defaultExplosiveBossEnemySpeed + enemiesStats.defaultExplosiveBossEnemySpeed * ((float)(currentWave)*1f/100f));
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        GameEventManager.instance.bossSpawn();
                        break;
                }
            }
            timeAtLastGroupSpawn= Time.time;
        }
    }
    public void onStartWave(){
        currentWave++;
        isSpawning= true;
    }
}
