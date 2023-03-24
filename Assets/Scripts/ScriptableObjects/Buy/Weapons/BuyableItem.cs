using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreateBuyable", menuName = "Buyable")]
public class BuyableItem : ScriptableObject
{
    [Header("Name")]
    public new string name;
    public string description;


    public GunInfo gunInfo;

    [Header("Price")]
    public uint price;
    public bool isAvaliable;
}