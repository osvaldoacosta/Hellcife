using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooProjectileHandler : MonoBehaviour
{
    public GameObject gooProjectile;
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }
    public void launchGooProjectile(Vector3 launchPoint, 
    float parabolaMaxHeight,
    Vector3 gooProjectileLandingCoord,
    float gooProjectileAirTime,
    float gooPuddleRadius,
    float gooPuddleDuration){
        //TO-DO
        //Implement an object pool instead of instantiating every new shot
        GameObject newProjectile= Instantiate(gooProjectile, launchPoint, Quaternion.identity);
        GooProjectileBehaviour gooProjectileBehaviour = newProjectile.GetComponent<GooProjectileBehaviour>();
        gooProjectileBehaviour.setGooShot(parabolaMaxHeight, gooProjectileLandingCoord, gooProjectileAirTime, gooPuddleRadius, gooPuddleDuration);
    }
}