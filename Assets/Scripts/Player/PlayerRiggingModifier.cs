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

    [SerializeField] private Rig pistolAimRig;
    [SerializeField] private Rig pistolIdleRig;
    [SerializeField] private Rig pistolShootRig;

    private Rig aimRig;
    private Rig shootRig;

    private float aimRigWeight;
    private float shootWeight;
    private Gun currentGun;
    private void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();
    }

    private void Update()
    {
        
        if (aimRig != null && aimRig.weight != aimRigWeight)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 10f);
            if (shootRig != null && shootRig.weight >= 0.99f)
            {
                shootWeight = 0f;
            }
            shootRig.weight = Mathf.Lerp(shootRig.weight, shootWeight, Time.deltaTime * 30f);
        }

        
    }


    //qd for pistola - mudar ref_right_hand pro da pistola, remover o ref_left e deixar o hint weight em 0.163

    private void ChangeToPistolIdlePosition()
    {
        rightArmIK.data.target = pistolRefRightHand;
        leftArmIK.data.target = null;
        rigBuilder.Build();
    }

    private void ChangeToAimingPistol()
    {
        aimRig = pistolAimRig;
        leftArmIK.data.target = pistolRefLeftHand;
        rigBuilder.Build();
    }
    
    private void ChangeToShootingPistol()
    {
        shootRig = pistolShootRig;
    }

    //qd for shotgun - mudar o ref_right_hand e o left hand, deixar o hint right em 0.716

    private void ChangeToShotgunIdlePosition()
    {
        
    }
    //qd for carabina - mudar o ref_right_hand e o left pro da carabina, e deixar os hints no máximo

    private void ChangeToRifleIdlePosition()
    {
        
    }
    //qd for nada - botar tudo zerado, hint e weight

    private void ChangeToBareHands()
    {
        
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
            if (currentGun == pistol)
            {
                ChangeToShootingPistol();
            }
        }
        
    }

 



}
