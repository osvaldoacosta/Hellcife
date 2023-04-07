using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<GroupSpawnData> groupsToSpawn= new List<GroupSpawnData>();
    public bool isSpawning= false;
    public GroupSpawnData currentGroup;

    float timeAtLastGroupSpawn= -1f;
    [SerializeField] public ObjectPool gooEnemyPool;
    [SerializeField] public ObjectPool meleeEnemyPool;
    [SerializeField] public ObjectPool explosiveEnemyPool;

    public void Start(){
        GameEventManager.instance.onStartWave+= onStartWave;
        gooEnemyPool= GameObject.FindGameObjectWithTag("GooEnemyObjectPool").GetComponent<ObjectPool>();
        meleeEnemyPool= GameObject.FindGameObjectWithTag("MeleeEnemyObjectPool").GetComponent<ObjectPool>();
        explosiveEnemyPool= GameObject.FindGameObjectWithTag("ExplosiveEnemyObjectPool").GetComponent<ObjectPool>();
    }

    public void LateUpdate(){
        if(isSpawning){
            if(groupsToSpawn.Count== 0){
                isSpawning= false;
                return;
            }
            if(groupsToSpawn[0].cooldownBeforeThisSpawn + timeAtLastGroupSpawn > Time.time){
                return;
            }
            currentGroup= groupsToSpawn[0];
            groupsToSpawn.RemoveAt(0);
            foreach(EnemySpawnData enemy in currentGroup.enemyGroup){
                GameObject newEnemy;
                switch(enemy.enemyType){
                    case "gooEnemy":
                        newEnemy = gooEnemyPool.GetPooledObject();
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                    case "meleeEnemy":
                        newEnemy = meleeEnemyPool.GetPooledObject();
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                    case "explosiveEnemy":
                        newEnemy = explosiveEnemyPool.GetPooledObject();
                        newEnemy.transform.position = transform.position;
                        newEnemy.transform.rotation = transform.rotation;
                        newEnemy.SetActive(true);
                        break;
                }
            }
            if(groupsToSpawn.Count== 0){
                isSpawning= false;
                return;
            }
            timeAtLastGroupSpawn= Time.time;
        }
    }
    public void onStartWave(){
        isSpawning= true;
    }
}
