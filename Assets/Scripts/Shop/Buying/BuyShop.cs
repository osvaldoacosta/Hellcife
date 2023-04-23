using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShop : Shop
{
  

    public void Buy(BuyableItem item)
    {

        if (item.isAvaliable)
        {
            if (CanPlayerBuy(item.price))
            {
                SpendPlayerPointsOnBuy(item.price);
            }
        }

        
    }
}
