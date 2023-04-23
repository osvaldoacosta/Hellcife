using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunInfo : ScriptableObject
{
    [Header("Name")]
    public new string name;
    public string description;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public bool isAutomatic;
    public bool wasShot;
    public ushort bulletsPerShot;
    
    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float roundsPerMinute;
    public float reloadTime;
    public bool isMagReloaded;
    public bool isReloading;
    public bool isAiming;

    
    
}
