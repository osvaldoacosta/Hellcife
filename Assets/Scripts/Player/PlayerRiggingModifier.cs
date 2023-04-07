using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;




public class PlayerRiggingModifier : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftArmIK;
    [SerializeField] private TwoBoneIKConstraint rightArmIK;
    [SerializeField] private Gun pistol;
    [SerializeField] private Transform pistolRefRightHand;
    [SerializeField] private Gun shotgun;
    [SerializeField] private Transform shotgunRefRightHand;
    [SerializeField] private Transform shotgunRefLeftHand;
    [SerializeField] private Gun rifle;
    [SerializeField] private Transform rifleRefRightHand;
    [SerializeField] private Transform rifleRefLeftHand;

    private RigBuilder rigBuilder;
    //qd for pistola - mudar ref_right_hand pro da pistola, remover o ref_left e deixar o hint weight em 0.163

    private void Awake()
    {
        
        rigBuilder = GetComponent<RigBuilder>();
    }

    private void ChangeToPistolIdlePosition(Gun gun)
    {
        leftArmIK.weight= 0f;
        rightArmIK.weight= 1f;
        rightArmIK.data.hintWeight= 0.163f;
        leftArmIK.data.hintWeight = 0f;
        rightArmIK.data.target = pistolRefRightHand;
        leftArmIK.data.target = null;
        rigBuilder.Build();//Da o rebuild dos rig tudo

    }
    //qd for shotgun - mudar o ref_right_hand e o left hand, deixar o hint right em 0.716

    private void ChangeToShotgunIdlePosition(Gun gun)
    {
        leftArmIK.weight = 1f;
        rightArmIK.weight = 1f;
        rightArmIK.data.hintWeight = 0.716f;
        leftArmIK.data.hintWeight = 1f;
        rightArmIK.data.target = shotgunRefRightHand;
        leftArmIK.data.target = shotgunRefLeftHand;
        rigBuilder.Build();//Da o rebuild dos rig tudo
    }
    //qd for carabina - mudar o ref_right_hand e o left pro da carabina, e deixar os hints no máximo

    private void ChangeToRifleIdlePosition(Gun gun)
    {
        leftArmIK.weight = 1f;
        rightArmIK.weight = 1f;
        rightArmIK.data.hintWeight = 1f;
        leftArmIK.data.hintWeight = 1f;
        rightArmIK.data.target = rifleRefRightHand;
        leftArmIK.data.target = rifleRefLeftHand;
        rigBuilder.Build();//Da o rebuild dos rig tudo
    }
    //qd for nada - botar tudo zerado, hint e weight

    private void ChangeToBareHands()
    {
        leftArmIK.weight= 0f;
        rightArmIK.weight = 0f;
        rightArmIK.data.hintWeight = 0f;
        leftArmIK.data.hintWeight = 0f;

    }

    public void ChangeWeaponRig(Gun gunToEquip)
    {
        Debug.Log(gunToEquip?.name);
        if(gunToEquip == null) {
            ChangeToBareHands();
        }
        else if (gunToEquip.Equals(pistol))
        {
            Debug.Log("Entrou");
            ChangeToPistolIdlePosition(gunToEquip);
        }
        else if (gunToEquip.Equals(shotgun))
        {
            ChangeToShotgunIdlePosition(gunToEquip);
        }
        else if(gunToEquip.Equals(rifle)) {
            ChangeToRifleIdlePosition(gunToEquip);
        }


        


    }
}
