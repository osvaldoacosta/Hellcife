using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooProjectileShooter : MonoBehaviour
{
    public GameObject gooProjectileObjectPool;
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
        //get projectile from object pool
        GameObject newGooProjectile= gooProjectileObjectPool.GetComponent<ObjectPool>().GetPooledObject(); 
        if (newGooProjectile != null) {
            newGooProjectile.transform.position = transform.position;
            newGooProjectile.transform.rotation = transform.rotation;
        }
        GooProjectileBehaviour gooProjectileBehaviour = newGooProjectile.GetComponent<GooProjectileBehaviour>();
        gooProjectileBehaviour.setGooShot(launchPoint ,parabolaMaxHeight, gooProjectileLandingCoord, gooProjectileAirTime, gooPuddleRadius, gooPuddleDuration);
    }
}