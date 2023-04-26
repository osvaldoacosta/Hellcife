using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    public string enemyType;
    public EnemySpawnData(string enemyType){
        this.enemyType = enemyType;
    }
    public EnemySpawnData(EnemySpawnData otherEnemySpawnData){
        this.enemyType = otherEnemySpawnData.enemyType;
    }
}
