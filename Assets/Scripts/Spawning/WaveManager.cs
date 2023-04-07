using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNumber;
    List<GameObject> portals;
    List<WaveData> allWaves;
    WaveData waveThatWillSpawnNext;
    [SerializeField] float timeUntilNextWaveSpawns;
    float timeAtLastWaveSpawn= -1f;
    [SerializeField] bool isCountingNextWaveCountdown= false;

    [SerializeField] private bool allPortalsFinishedSpawning= false;

    void Start(){
        GameEventManager.instance.onStartWave+= onStartWave;
        waveNumber= 0;

        portals= new List<GameObject>();
        foreach(GameObject portal in GameObject.FindGameObjectsWithTag("Portal")){
            portals.Add(portal);
        }

        

        //TESTING SPAWN
        EnemySpawnData gooEnemySpawnData= new EnemySpawnData("gooEnemy");
        EnemySpawnData meleeEnemySpawnData= new EnemySpawnData("meleeEnemy");
        EnemySpawnData explosiveEnemySpawnData= new EnemySpawnData("explosiveEnemy");
        EnemySpawnData gooBossEnemySpawnData= new EnemySpawnData("gooBossEnemy");
        EnemySpawnData meleeBossEnemySpawnData= new EnemySpawnData("meleeBossEnemy");
        EnemySpawnData explosiveBossEnemySpawnData= new EnemySpawnData("explosiveBossEnemy");
        GroupSpawnData gooEnemyGroup = new GroupSpawnData(10f);
        GroupSpawnData meleeEnemyGroup = new GroupSpawnData(10f);
        GroupSpawnData explosiveEnemyGroup = new GroupSpawnData(12f);
        GroupSpawnData gooBossEnemyGroup = new GroupSpawnData(30f);
        GroupSpawnData meleeBossEnemyGroup = new GroupSpawnData(30f);
        GroupSpawnData explosiveBossEnemyGroup = new GroupSpawnData(36f);
        WaveData newWave = new WaveData(20f);
        List<WaveData> newAllWaves = new List<WaveData>();

        gooEnemyGroup.addEnemyToGroup(gooEnemySpawnData, 3);
        meleeEnemyGroup.addEnemyToGroup(meleeEnemySpawnData, 3);
        explosiveEnemyGroup.addEnemyToGroup(explosiveEnemySpawnData, 3);
        gooBossEnemyGroup.addEnemyToGroup(gooEnemySpawnData, 1);
        meleeBossEnemyGroup.addEnemyToGroup(meleeEnemySpawnData, 1);
        explosiveBossEnemyGroup.addEnemyToGroup(explosiveBossEnemySpawnData, 1);

        gooEnemyGroup.setCooldownAfterThisSpawn(10f);
        meleeEnemyGroup.setCooldownAfterThisSpawn(10f);
        explosiveEnemyGroup.setCooldownAfterThisSpawn(10f);
        gooBossEnemyGroup.setCooldownAfterThisSpawn(60f);
        meleeBossEnemyGroup.setCooldownAfterThisSpawn(60f);
        explosiveBossEnemyGroup.setCooldownAfterThisSpawn(60f);

        newWave.addGroupToWave(gooEnemyGroup, 1);
        newWave.addGroupToWave(meleeEnemyGroup, 1);
        newWave.addGroupToWave(explosiveEnemyGroup, 1);

        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(120f);
        newWave.addGroupToWave(meleeBossEnemyGroup, 1);
        newWave.addGroupToWave(meleeEnemyGroup, 3);
        newAllWaves.Add(newWave);

        //wave 11
        newWave= new WaveData(20f);
        newWave.addGroupToWave(gooEnemyGroup, 1);
        newWave.addGroupToWave(meleeEnemyGroup, 1);
        newWave.addGroupToWave(explosiveEnemyGroup, 1);

        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(120f);
        newWave.addGroupToWave(gooBossEnemyGroup, 1);
        newWave.addGroupToWave(gooEnemyGroup, 3);
        newAllWaves.Add(newWave);

        //wave 21
        newWave= new WaveData(20f);
        newWave.addGroupToWave(gooEnemyGroup, 1);
        newWave.addGroupToWave(meleeEnemyGroup, 1);
        newWave.addGroupToWave(explosiveEnemyGroup, 1);

        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(newWave);
        newAllWaves.Add(newWave);
        newWave= new WaveData(120f);
        newWave.addGroupToWave(explosiveBossEnemyGroup, 1);
        newWave.addGroupToWave(explosiveEnemyGroup, 3);
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
                
            }
            if(allPortalsFinishedSpawning){
                Debug.Log("FINISHED SPAWNING!");
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
        allPortalsFinishedSpawning= false;
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
