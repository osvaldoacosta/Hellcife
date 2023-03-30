using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstaConsumable", menuName = "Item/Consumable/InstantUse")]
public class InstaConsumable : BuyableItem
{
    [Header("Stats")]
    public ushort recoverValue;

}
