using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData
{
    public List<GroupSpawnData> enemyWave {get; private set;}
    public float cooldownUntilNextWave;

    public WaveData(float cooldownUntilNextWave){
        this.enemyWave = new List<GroupSpawnData>();
        this.cooldownUntilNextWave = cooldownUntilNextWave;
    }
    public WaveData(WaveData otherWaveData){
        this.enemyWave = new List<GroupSpawnData>(otherWaveData.enemyWave);
        this.cooldownUntilNextWave = otherWaveData.cooldownUntilNextWave;
    }
    public void addGroupToWave(GroupSpawnData group, int quantity){
        if(quantity<=0) return;
        if(group.enemyGroup.Count <= 0) return;

        
        for(int i = 0; i < quantity; i++){
            GroupSpawnData newGroup = new GroupSpawnData(group);
            if(enemyWave != null){
                enemyWave.Add(newGroup);
            }
        }
    }
}
