using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateUpgrade", menuName = "Upgrade")]
public class UpgradeInfo:ScriptableObject
{
    [Header("Name")]
    public new string name;
    public string description;

    [Header("Using")]
    public ushort useLimit;
    public ushort valueToAdd;

    [Header("Price")]
    public uint price;
}
