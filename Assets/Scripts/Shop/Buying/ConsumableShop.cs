using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConsumableShop : BuyShop
{

    private Dictionary<BuyableItem, Action<BuyableItem>> consumableFunctions;
    

    private void HealPlayer(InstaConsumable consumable) => GetShopInteraction().HealPlayer(consumable.recoverValue);

    private void UseEffectInPlayer(EffectConsumable consumable) =>
        StartCoroutine(GetShopInteraction().UseItemWithEffect(consumable));

    

    public void BuyInstaConsumable(InstaConsumable consumable)
    {
        Buy(consumable);
        HealPlayer(consumable);
    }


    public void BuyEffectConsumable(EffectConsumable consumable)
    {
        Buy(consumable);
        UseEffectInPlayer(consumable);
        
    }

}
