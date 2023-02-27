using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class GooProjectileBehaviour : MonoBehaviour
{   
    private bool isBeingLaunched=false;

    private Vector3 startingPoint;
    private Vector3 landingPoint;
    
    private float timeAtLaunch=-1f;
    private float timeSinceLaunch=-1f;
    private float totalLaunchTime=-1f;

    float totalXTravel;
    float totalYTravel;
    float totalZTravel;

    private float totalHorizontalTravel;

    private float parabolaA;
    private float parabolaB;
    private float parabolaCompletionPercentage;

    float frameXRelativePosition;
    float frameYRelativePosition;
    float frameZRelativePosition;

    private float newGooPuddleRadius;
    private float newGooPuddleDuration;

    public GameObject gooPuddleObjectPool;
    private GameObject newGooPuddle;

    // Update is called once per frame
    void Start(){
        gooPuddleObjectPool= GameObject.FindWithTag("GooPuddleObjectPool");
    }
    void Update()
    {
        if(isBeingLaunched){
            timeSinceLaunch= Time.time - timeAtLaunch;
            parabolaCompletionPercentage = timeSinceLaunch/totalLaunchTime;
            frameXRelativePosition= totalXTravel*(parabolaCompletionPercentage);
            frameZRelativePosition= totalZTravel*(parabolaCompletionPercentage);

            frameYRelativePosition= totalYTravel*(parabolaCompletionPercentage);
            frameYRelativePosition+= parabolaA*((float)System.Math.Pow(totalHorizontalTravel*(parabolaCompletionPercentage), 2)) + parabolaB*(totalHorizontalTravel*(parabolaCompletionPercentage)); 
            transform.position= startingPoint + new Vector3(frameXRelativePosition, frameYRelativePosition, frameZRelativePosition);
            if(timeSinceLaunch>=totalLaunchTime){
                isBeingLaunched= false;
                //making sure it lands on the intended spot, taking into account that there is imprecision on the calculation of the parabola coords
                transform.position= landingPoint;
                createGooPuddle(landingPoint, newGooPuddleRadius, newGooPuddleDuration);
                this.gameObject.SetActive(false);
            }
        }
    }
    void OnEnable(){
        this.gameObject.GetComponent<TrailRenderer>().Clear();
    }

    public void setGooShot(Vector3 launchPoint,float parabolaMaxHeight, Vector3 gooProjectileLandingCoord, float gooProjectileAirTime, float gooPuddleRadius, float gooPuddleDuration){
        startingPoint= launchPoint;
        landingPoint= gooProjectileLandingCoord;
        
        totalXTravel = gooProjectileLandingCoord.x-launchPoint.x;
        totalZTravel = gooProjectileLandingCoord.z-launchPoint.z;
        totalHorizontalTravel= (float)System.Math.Sqrt(System.Math.Pow(totalXTravel, 2) + System.Math.Pow(totalZTravel, 2));
        totalYTravel = gooProjectileLandingCoord.y-launchPoint.y;

        parabolaA= -1*(parabolaMaxHeight)/((float)System.Math.Pow(totalHorizontalTravel/2, 2));
        parabolaB= -2*(totalHorizontalTravel/2)*parabolaA;

        timeAtLaunch= Time.time;
        totalLaunchTime= gooProjectileAirTime;

        newGooPuddleDuration= gooPuddleDuration;
        newGooPuddleRadius= gooPuddleRadius;

        isBeingLaunched= true;
        this.gameObject.SetActive(true);
    }
    private void createGooPuddle(Vector3 gooPuddleCoord, float gooPuddleRadius, float gooPuddleDuration){
        newGooPuddle= gooPuddleObjectPool.GetComponent<ObjectPool>().GetPooledObject(); 
        if (newGooPuddle != null) {
            newGooPuddle.transform.position = gooPuddleCoord;
            newGooPuddle.transform.rotation = transform.rotation;
            newGooPuddle.GetComponent<GooPuddleBehaviour>().setGooPuddle(gooPuddleDuration, gooPuddleRadius);
            newGooPuddle.SetActive(true);
        }
    }
}
