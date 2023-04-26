using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;
    public event Action onStartWave;
    public event Action onBossSpawn;
    public event Action onGameOver;

    public void Awake(){
        if(instance != null){
            return;
        }
        instance = this;
    }

    public void startWave(){
        if(onStartWave!=null){
            onStartWave();
        }
    }
    public void bossSpawn(){
        if(onBossSpawn!=null){
            onBossSpawn();
        }
    }
    public void gameOver(){
        if(onGameOver!=null){
            onGameOver();
        }
    }
}
