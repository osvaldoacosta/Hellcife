using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicBehaviour : MonoBehaviour
{
    public bool isBossMusicPlaying = false;
    AudioSource bossMusicSource;
    AudioSource normalMusicSource;

    ObjectPool meleeBossPool;
    ObjectPool gooBossPool;
    ObjectPool explosiveBossPool;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.onBossSpawn += onBossSpawn;
        GameEventManager.instance.onGameOver += onGameOver;

        bossMusicSource= GameObject.FindGameObjectsWithTag("BossMusicSource")[0].GetComponent<AudioSource>();
        bossMusicSource.mute= true;
        normalMusicSource= GameObject.FindGameObjectsWithTag("NormalMusicSource")[0].GetComponent<AudioSource>();

        meleeBossPool= GameObject.FindGameObjectWithTag("MeleeEnemyObjectPool").GetComponent<ObjectPool>();
        gooBossPool= GameObject.FindGameObjectWithTag("GooBossEnemyPool").GetComponent<ObjectPool>();
        explosiveBossPool= GameObject.FindGameObjectWithTag("ExplosiveBossEnemyPool").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBossMusicPlaying){
            bool isAnyBossAlive= meleeBossPool.hasAnyEnabled() || gooBossPool.hasAnyEnabled() || explosiveBossPool.hasAnyEnabled();
            if(!(isAnyBossAlive)){
                endBossMusic();
            }
        }
    }
    public void onGameOver(){
        bossMusicSource.pitch= 0.7f;
    }
    public void onBossSpawn(){
        foreach (Transform soundTrackTransform in transform){
            soundTrackTransform.gameObject.GetComponent<AudioSource>().mute= true;
        }
        bossMusicSource.mute= false;
        isBossMusicPlaying= true;
    }
    public void endBossMusic(){
        bossMusicSource.mute= true;
        normalMusicSource.mute= false;
        isBossMusicPlaying= false;
    }
}
