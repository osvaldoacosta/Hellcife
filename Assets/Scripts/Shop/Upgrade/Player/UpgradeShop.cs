using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : Shop
{


    private Dictionary<UpgradeInfo, UpgradeInfo> referenceToInstantiadedDictionary;


    
    public void BuyUpgrade(UpgradeInfo referenceUpgrade, Dictionary<UpgradeInfo, Action<UpgradeInfo>> upgradeDictionary)
    {
        UpgradeInfo instantiatedUpgrade = referenceToInstantiadedDictionary[referenceUpgrade];

        if (instantiatedUpgrade.useLimit > 0)
        {
            if (CanPlayerBuy(instantiatedUpgrade.price))
            {
                SpendPlayerPointsOnBuy(instantiatedUpgrade.price);
                upgradeDictionary[referenceUpgrade](instantiatedUpgrade); //Executa o metodo respectivo ao objeto do upgrade fornecido, ex: Scriptable: UpgradeGunDmg -> Metodo: IncreaseGunDmg().
                instantiatedUpgrade.useLimit -= 1; //Como vai mudar o valor, mudar o valor do upgrade intanciado e não o original
                instantiatedUpgrade.price *= 2; //O preço duplica após a compra
            }
            else
            {
                //Meter um pop-up dizendo que o jogador está ruim das contas? >_<
            }
        }
    }

    //Esse metodo serve pra linkar um scriptable object a ao seu valor instanciado, deixando assim a key intocada.
    public Dictionary<UpgradeInfo, UpgradeInfo> CreateReferenceToInstantiatedDictionary(HashSet<UpgradeInfo> keys)
    {
        if(referenceToInstantiadedDictionary == null)
        {
            referenceToInstantiadedDictionary = new Dictionary<UpgradeInfo, UpgradeInfo>(); 
        }

        foreach (UpgradeInfo upgrade in keys)
        {
            UpgradeInfo info = Instantiate(upgrade);
            
            referenceToInstantiadedDictionary.Add(upgrade, info);
        }

        return referenceToInstantiadedDictionary;
    }


    

   




}
