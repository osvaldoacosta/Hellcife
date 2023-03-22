using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUpgradeShop: UpgradeShop
{
    public void BuyTapioca(TapiocaInfo tapioca)
    {
        if (tapioca.useLimit > 0)
        {
            if (CanPlayerBuy(tapioca.price))
            {
                SpendPlayerPointsOnBuy(tapioca.price);
                GetTapiocaMethods()[tapioca](tapioca);
                tapioca.useLimit -= 1;
            }
            else
            {
                //Meter um pop-up? >_<
            }
        }
    }

    //Referencia ao tapioca info ligada no botão do unity
    public void OnTapiocaButtonClicked(TapiocaInfo tapioca)
    {
        Debug.Log(tapioca.price);
        BuyTapioca(tapioca);
    }




}
