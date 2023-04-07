using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawnData
{
    public List<EnemySpawnData> enemyGroup {get; private set;}

    public float cooldownBeforeThisSpawn {get; private set;}

    public GroupSpawnData(GroupSpawnData otherGroup){
        this.cooldownBeforeThisSpawn= otherGroup.cooldownBeforeThisSpawn;

        this.enemyGroup = new List<EnemySpawnData>(otherGroup.enemyGroup);
    }
    public GroupSpawnData(float cooldownBeforeThisSpawn){
        this.enemyGroup= new List<EnemySpawnData>();
        this.cooldownBeforeThisSpawn= cooldownBeforeThisSpawn;
    }
    public GroupSpawnData(){
        this.enemyGroup= new List<EnemySpawnData>();
        this.cooldownBeforeThisSpawn= 0f;
    }

    public void addEnemyToGroup(EnemySpawnData enemy, int quantity){
        if(quantity<=0) return;
        if(enemy.enemyType == null) return;
        EnemySpawnData newEnemy = new EnemySpawnData(enemy);

        for(int i = 0; i < quantity; i++){
            enemyGroup.Add(newEnemy);
        }
    }
    public void setCooldownBeforeThisSpawn(float cooldownBeforeThisSpawn){
        this.cooldownBeforeThisSpawn= cooldownBeforeThisSpawn;
    }
    
}
