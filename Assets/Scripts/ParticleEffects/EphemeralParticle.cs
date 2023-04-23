using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EphemeralParticle : MonoBehaviour
{
    [SerializeField] public float duration;
    float timeToStopEmmiting= -1f;
    bool isEmmiting = false;
    
    
    public void Emit(int numberOfParticles){
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        if(ps==null) return;
        gameObject.SetActive(true);
        isEmmiting = true;
        timeToStopEmmiting= Time.time + duration;
        ps.Emit(numberOfParticles);

    }
    public void Emit(int numberOfParticles, float duration){
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
        if(ps==null) return;
        gameObject.SetActive(true);
        isEmmiting = true;
        timeToStopEmmiting= Time.time + duration;
        ps.Emit(numberOfParticles);

    }
    // Update is called once per frame
    void Update()
    {
        if(isEmmiting){
            if(Time.time >= timeToStopEmmiting){
                gameObject.SetActive(false);
                isEmmiting= false;
            }
        }
    }
}
