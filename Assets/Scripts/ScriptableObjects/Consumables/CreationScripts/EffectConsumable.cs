using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InstaConsumable", menuName = "Item/Consumable/EffectUse")]
public class EffectConsumable : ConsumableBaseObject
{
    
    [Header("Stats")]
    public ushort healIncrementPerc;
    public ushort speedIncrementPerc;
    public ushort damageIncrementPerc;

    public float effectDuration;

}