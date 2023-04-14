using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.XR;




public class PlayerRiggingModifier : MonoBehaviour
{
    [SerializeField] private TwoBoneIKConstraint leftArmIK;
    [SerializeField] private TwoBoneIKConstraint rightArmIK;
    [SerializeField] private Gun pistol;
    [SerializeField] private Transform pistolRefRightHand;
    [SerializeField] private Transform pistolRefLeftHand;
    [SerializeField] private Gun shotgun;
    [SerializeField] private Transform shotgunRefRightHand;
    [SerializeField] private Transform shotgunRefLeftHand;
    [SerializeField] private Gun rifle;
    [SerializeField] private Transform rifleRefRightHand;
    [SerializeField] private Transform rifleRefLeftHand;


    private RigBuilder rigBuilder;

    [SerializeField] private Rig aimRig;
    [SerializeField] private Rig idleRig;
    [SerializeField] private Rig shootRig;


    

    private float aimRigWeight;
    private float shootWeight;
    private Gun currentGun;

    private void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();
    }

    public void SetIdleRigWeight(float weight)
    {
        if(idleRig != null)
        {
            idleRig.weight = weight;
        }
    }

    private void Update()
    {
        
        if (aimRig != null && aimRig.weight != aimRigWeight)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 10f);
            if (shootRig != null)
            {
                if (shootRig.weight >= 0.99f) shootWeight = 0f;
                shootRig.weight = Mathf.Lerp(shootRig.weight, shootWeight, Time.deltaTime * 30f);
            }
        }

        
    }



    private void ChangeToPistolIdlePosition()
    {
        
        rightArmIK.data.target = pistolRefRightHand;
        leftArmIK.data.target = null;
        rigBuilder.Build();
    }

    private void ChangeToAimingPistol()
    {
        
        leftArmIK.data.target = pistolRefLeftHand;
        rigBuilder.Build();
    }
    
    


    private void ChangeToShotgunIdlePosition()
    {
        
        rightArmIK.data.target = shotgunRefRightHand;
        leftArmIK.data.target = shotgunRefLeftHand;
        rigBuilder.Build();
    }

    private void ChangeToRifleIdlePosition()
    {
        rightArmIK.data.target = rifleRefRightHand;
        leftArmIK.data.target = rifleRefLeftHand;
        rigBuilder.Build();
    }


    private void ChangeToBareHands()
    {
        rightArmIK.data.target = null;
        leftArmIK.data.target = null;
        rigBuilder.Build();
    }

    public void ChangeWeaponRig(Gun gunToEquip)
    {
        Debug.Log(gunToEquip?.name);
        if(gunToEquip == null) {
            currentGun= null;
            ChangeToBareHands();
        }
        else if (gunToEquip.Equals(pistol))
        {
            currentGun = pistol;
            ChangeToPistolIdlePosition();
        }
        else if (gunToEquip.Equals(shotgun))
        {
            currentGun = shotgun;
            ChangeToShotgunIdlePosition();
        }
        else if(gunToEquip.Equals(rifle)) {
            currentGun= rifle;
            ChangeToRifleIdlePosition();
        }

    }


    public void OnAimStart()
    {
        if (currentGun != null)
        {
            aimRigWeight = 1f;
            if (currentGun == pistol)
            {
                ChangeToAimingPistol();
            }
        }
    }

    public void OnAimEnd()
    {
        if (currentGun != null)
        {
            aimRigWeight = 0f;
            if (currentGun == pistol)
            {
                ChangeToPistolIdlePosition();
            }
        }
    }

    public void OnGunShoot()
    {
        if (currentGun != null)
        {
            shootWeight = 1f;
        }
        
    }

 



}
