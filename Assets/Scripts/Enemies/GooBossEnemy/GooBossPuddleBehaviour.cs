using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBossPuddleBehaviour : MonoBehaviour
{
    float timeToDeactivate;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= timeToDeactivate){
            gameObject.SetActive(false);
        }
    }
    public void setGooPuddle(float gooPuddleDuration, float gooPuddleRadius){
        setDeactivateTime(gooPuddleDuration+Time.time);
        setRadius(gooPuddleRadius);
    }
    private void setRadius(float radius){
        transform.localScale= new Vector3(radius, transform.localScale.y, radius);
    }
    public void setDeactivateTime(float deactivateTime){
        timeToDeactivate= deactivateTime;
    }
}
