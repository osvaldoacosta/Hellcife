using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstaConsumable", menuName = "Item/Consumable/InstantUse")]
public class InstaConsumable : ConsumableBaseObject
{
    [Header("Stats")]
    public ushort healRecover;

}
