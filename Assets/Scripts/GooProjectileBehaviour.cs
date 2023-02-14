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

    float frameXRelativePosition;
    float frameYRelativePosition;
    float frameZRelativePosition;

    private float newGooPuddleRadius;
    private float newGooPuddleDuration;
    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingLaunched){
            float completePercentage = timeSinceLaunch/totalLaunchTime;
            timeSinceLaunch= Time.time - timeAtLaunch;
            frameXRelativePosition= totalXTravel*(completePercentage);
            frameZRelativePosition= totalZTravel*(completePercentage);

            frameYRelativePosition= totalYTravel*(completePercentage);
            frameYRelativePosition+= parabolaA*((float)System.Math.Pow(totalHorizontalTravel*(completePercentage), 2)) + parabolaB*(totalHorizontalTravel*(completePercentage)); 
            transform.position= startingPoint + new Vector3(frameXRelativePosition, frameYRelativePosition, frameZRelativePosition);
            if(timeSinceLaunch>=totalLaunchTime){
                isBeingLaunched= false;
                //making sure it lands on the intended spot, taking into account that there is imprecision on the calculation of the parabola coords
                transform.position= landingPoint;
                createAndManageGooPuddle(landingPoint, newGooPuddleRadius, newGooPuddleDuration);
            }
        }
    }

    public void setGooShot(float parabolaMaxHeight, Vector3 gooProjectileLandingCoord, float gooProjectileAirTime, float gooPuddleRadius, float gooPuddleDuration){
        startingPoint= transform.position;
        landingPoint= gooProjectileLandingCoord;
        
        totalXTravel = gooProjectileLandingCoord.x-transform.position.x;
        totalZTravel = gooProjectileLandingCoord.z-transform.position.z;
        totalHorizontalTravel =  (float)System.Math.Sqrt(System.Math.Pow(totalXTravel, 2) + System.Math.Pow(totalZTravel, 2));
        totalYTravel = gooProjectileLandingCoord.y-transform.position.y;

        parabolaA= -1*(parabolaMaxHeight)/((float)System.Math.Pow(totalHorizontalTravel/2, 2));
        parabolaB= -2*(totalHorizontalTravel/2)*parabolaA;

        timeAtLaunch= Time.time;
        totalLaunchTime= gooProjectileAirTime;

        newGooPuddleDuration= gooPuddleDuration;
        newGooPuddleRadius= gooPuddleRadius;

        isBeingLaunched= true;
    }
    private void createAndManageGooPuddle(Vector3 gooProjectileLandingCoord, float gooPuddleRadius, float gooPuddleDuration){
        return;
    }
}
