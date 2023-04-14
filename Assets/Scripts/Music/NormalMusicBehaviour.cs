using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMusicBehaviour : MonoBehaviour
{
    AudioSource normalMusicSource;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.instance.onGameOver += onGameOver;
        normalMusicSource= this.GetComponent<AudioSource>();
    }

    public void onGameOver(){
        normalMusicSource.pitch= 0.7f;
    }
}
