using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunUpgrade", menuName = "Upgrade/Gun")]
public class GunUpgradeInfo:ScriptableObject
{
    [Header("Name")]
    public new string name;
    

    [Header("Damage")]
    public ushort damageQtyLimit;
    public string dmgDescription;
    public float dmgToAdd;

    [Header("FireRate")]
    public ushort firerateQtyLimit;
    public string firerateDescription;
    public float RPMToAdd;



    [Header("ReloadSpeed")]
    public ushort rldSpeedQtyLimit;
    public string rldSpeedDescription;
    public float rldSpeedToAdd;

    [Header("Capacity")]
    public ushort capacityLimit;
    public string capacityDescription;
    public float  capacityToAdd;

    [Header("Unique")]
    public ushort uniqueQtyLimit;
    public string uniqueDescription;

    [Header("Price")]
    public uint price;
}
