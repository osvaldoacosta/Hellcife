using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableItem : ScriptableObject
{
    [Header("Name")]
    public new string name;
    public string description;

    [Header("Price")]
    public uint price;
    public bool isAvaliable;
}
