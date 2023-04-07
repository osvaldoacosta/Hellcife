using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnData
{
    public string enemyType;
    public int enemyStrenght;
    public EnemySpawnData(string enemyType, int enemyStrenght){
        this.enemyType = enemyType;
        this.enemyStrenght = enemyStrenght;
    }
    public EnemySpawnData(EnemySpawnData otherEnemySpawnData){
        this.enemyType = otherEnemySpawnData.enemyType;
        this.enemyStrenght = otherEnemySpawnData.enemyStrenght;
    }
}
