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

    [SerializeField] private Gun other_pistol;
    [SerializeField] private Transform otherPistolRefRightHand;

    [SerializeField] private Gun shotgun;
    [SerializeField] private Transform shotgunRefRightHand;
    [SerializeField] private Transform shotgunRefLeftHand;
    [SerializeField] private Gun rifle;
    [SerializeField] private Transform rifleRefRightHand;
    [SerializeField] private Transform rifleRefLeftHand;

    [SerializeField] private Transform swordRightHand;
    private RigBuilder rigBuilder;

    [SerializeField] private Rig aimRig;
    [SerializeField] private Rig idleRig;
    [SerializeField] private Rig shootRig;
    [SerializeField] private Rig drawRig;

    

    private float aimRigWeight;
    private float shootRigWeight;
    private float drawRigWeight; 

    private Gun currentGun;

    private Animator animator;
    private void Awake()
    {
        rigBuilder = GetComponent<RigBuilder>();
        animator = GetComponent<Animator>();
    }

    public void SetIdleRigWeight(float weight)
    {
        if(idleRig != null)
        {
            idleRig.weight = weight;
        }
    }

    

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(PerformSwordSlash());
        }
        if (aimRig != null && aimRig.weight != aimRigWeight)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 10f);
            if (shootRig != null)
            {
                if (shootRig.weight >= 0.99f) shootRigWeight = 0f;
                shootRig.weight = Mathf.Lerp(shootRig.weight, shootRigWeight, Time.deltaTime * 30f);
            }
        }
        
        if(drawRig != null) {
            if (drawRig.weight >= 0.99f)
            {
                leftArmIK.weight = 1f;
                drawRigWeight = 0f;
            }
            drawRig.weight = Mathf.Lerp(drawRig.weight, drawRigWeight, Time.deltaTime * 15f);
            //if(currentGun != pistol)
            //leftArmIK.weight = Mathf.Lerp(leftArmIK.weight, 1 - drawRigWeight, Time.deltaTime * 20f);
        }
        
        
    }



    private void ChangeToPistolIdlePosition()
    {
        if(other_pistol != null) {
            leftArmIK.data.target = otherPistolRefRightHand;
        }
        else
        {
            leftArmIK.data.target = null;
        }
        rightArmIK.data.target = pistolRefRightHand;
        
        rigBuilder.Build();
    }
    //Como a pistola eh uma pegada diferente ela precisa ter o seu proprio metodo ao mirar.
    private void ChangeToAimingPistol()
    {
        if(otherPistolRefRightHand == null) leftArmIK.data.target = pistolRefLeftHand;

        rigBuilder.Build();
    }
    
    private IEnumerator PerformSwordSlash()
    {
        rightArmIK.data.target = swordRightHand;
        leftArmIK.data.target = null;
        rigBuilder.Build();
        animator.Play("sword_slash");
        yield return new WaitForSeconds(2.03f);
        ChangeWeaponRig(currentGun);
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
        
        if (gunToEquip == null) {
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
        
        drawRigWeight = 1f;
        drawRig.weight = 0f;
        //leftArmIK.weight = 0f;
    }


    public void OnAimStart()
    {
        if (currentGun != null)
        {
            aimRigWeight = 1f;
            rightArmIK.data.hintWeight = 0.453f; //Magic number pra arma ficar bonita no modelo do player
            if (other_pistol != null) leftArmIK.data.hintWeight = 0.453f;
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
            rightArmIK.data.hintWeight = 1f;
            if (other_pistol != null) leftArmIK.data.hintWeight = 1f;
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
            shootRigWeight = 1f;
        }
    }

    public void EquipDualies(Gun otherPistol,Transform right_hand )
    {
        this.other_pistol = otherPistol;
        otherPistolRefRightHand = right_hand;
    }
}
