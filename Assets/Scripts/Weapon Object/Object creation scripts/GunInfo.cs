using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunInfo : ScriptableObject
{
    [Header("Name")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float roundsPerMinute;
    public float reloadTime;

    [HideInInspector]
    public bool isReloading;
    public bool isAiming;
    
}
