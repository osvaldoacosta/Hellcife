using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber;
    List<GameObject> portals;
    List<WaveData> allWaves;
    WaveData waveThatWillSpawnNext;
    float timeUntilNextWaveSpawns;
    float timeAtLastWaveSpawn= -1f;
    bool isCountingNextWaveCountdown= false;

    [SerializeField] private bool allPortalsFinishedSpawning= false;

    void Start(){
        GameEventManager.instance.onStartWave+= onStartWave;
        waveNumber= 0;

        portals= new List<GameObject>();
        foreach(GameObject portal in GameObject.FindGameObjectsWithTag("Portal")){
            portals.Add(portal);
        }

        

        //TESTING SPAWN
        EnemySpawnData gooEnemySpawnData= new EnemySpawnData("gooEnemy", 1);
        EnemySpawnData meleeEnemySpawnData= new EnemySpawnData("meleeEnemy", 1);
        GroupSpawnData gooEnemyGroup = new GroupSpawnData(3f);
        GroupSpawnData meleeEnemyGroup = new GroupSpawnData(2f);
        WaveData newWave = new WaveData(10f);
        List<WaveData> newAllWaves = new List<WaveData>();

        gooEnemyGroup.addEnemyToGroup(gooEnemySpawnData, 3);
        meleeEnemyGroup.addEnemyToGroup(meleeEnemySpawnData, 5);

        gooEnemyGroup.setCooldownBeforeThisSpawn(3f);
        meleeEnemyGroup.setCooldownBeforeThisSpawn(1f);

        newWave.addGroupToWave(gooEnemyGroup, 2);
        newWave.addGroupToWave(meleeEnemyGroup, 2);

        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);

        allWaves= new List<WaveData>(newAllWaves);
        //END OF TESTING SPAWN

        waveThatWillSpawnNext= allWaves[0];
        allWaves.RemoveAt(0);
        GameEventManager.instance.startWave();
    }
    public void Update(){
        if(isCountingNextWaveCountdown){
            Debug.Log("NEXT WAVE AT: "+ (timeAtLastWaveSpawn + timeUntilNextWaveSpawns));
            if(Time.time > timeAtLastWaveSpawn + timeUntilNextWaveSpawns){
                isCountingNextWaveCountdown= false;
                GameEventManager.instance.startWave();
            }
        }
    }
    public void LateUpdate(){
        if(!allPortalsFinishedSpawning){
            allPortalsFinishedSpawning = true;
            foreach(GameObject portal in portals){
                allPortalsFinishedSpawning= allPortalsFinishedSpawning && !(portal.GetComponent<Portal>().isSpawning);
                if(allPortalsFinishedSpawning){
                    Debug.Log("FINISHED SPAWNING!");
                }
            }
            if(allPortalsFinishedSpawning){
                startNextWaveCountDown();
            }
        }
    }

    public void onStartWave(){
        waveNumber++;
        if(waveThatWillSpawnNext.enemyWave.Count>=0){
            timeUntilNextWaveSpawns= waveThatWillSpawnNext.cooldownUntilNextWave;
        }
        while(waveThatWillSpawnNext.enemyWave.Count>0){
            foreach (GameObject portal in portals)
            {
                if(waveThatWillSpawnNext.enemyWave.Count==0){
                    break;
                }
                portal.GetComponent<Portal>().groupsToSpawn.Add(waveThatWillSpawnNext.enemyWave[0]);
                waveThatWillSpawnNext.enemyWave.RemoveAt(0);
            }
        }
        allWaves.RemoveAt(0);
        if(allWaves.Count != 0){
            waveThatWillSpawnNext= allWaves[0];
        }
    }
    public void startNextWaveCountDown(){
        isCountingNextWaveCountdown= true;
        timeAtLastWaveSpawn= Time.time;
    }
    public void finishGame(){
        Debug.Log("FINISH!");
        return;
    }
}
