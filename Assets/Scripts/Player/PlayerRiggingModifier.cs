using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

//
public class PlayerRiggingModifier : MonoBehaviour
{
    [SerializeField] GameObject leftHandHint;
    [SerializeField] GameObject rightHandHint;
    [SerializeField] TwoBoneIKConstraint leftArmIK;
    [SerializeField] TwoBoneIKConstraint rightArmIK;


    public void ChangeToPistolIdlePosition()
    {
        
    }
    public void ChangeToShotgunIdlePosition()
    {

    }
    public void ChangeToRifleIdlePosition()
    {

    }
}
