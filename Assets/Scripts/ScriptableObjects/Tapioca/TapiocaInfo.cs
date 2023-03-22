using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tapioca", menuName = "Item/Tapioca")]
public class TapiocaInfo:ScriptableObject
{
    [Header("Name")]
    public new string name;
    [Header("Using")]
    public ushort useLimit;
    public ushort valueToAdd;
}
