using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpUpgradeShop: UpgradeShop
{

    [SerializeField] private UpgradeInfo tapiocaCharqueReference;
    [SerializeField] private UpgradeInfo tapiocaChickenReference;
    [SerializeField] private UpgradeInfo tapiocaCoconutReference;

    private Dictionary<UpgradeInfo, Action<UpgradeInfo>> upgradeBasedOnTapioca;
    
    private void IncreaseHP(UpgradeInfo tapioca) => GetShopInteraction().IncreasePlayerHp(tapioca.valueToAdd);
    private void IncreaseSpeed(UpgradeInfo tapioca) => GetShopInteraction().IncreasePlayerSpeed(tapioca.valueToAdd);
    private void IncreaseCarryingCapacity(UpgradeInfo tapioca) => GetShopInteraction().IncreaseCarryingCapacity();


    private void Awake()
    {
        CreateTapiocaUpgradeDictionary();
        CreateReferenceToInstantiatedDictionary(new HashSet<UpgradeInfo>(upgradeBasedOnTapioca.Keys));



        //TODO: Fazer com que as tapiocas disponiveis fiquei disponiveis na ui, e o resto fique cinzada
    }

    //Referencia ao tapioca info ligada no botão do unity
    public void OnTapiocaButtonClicked(UpgradeInfo tapioca)
    {
        BuyUpgrade(tapioca, upgradeBasedOnTapioca);
    }

    


    private void CreateTapiocaUpgradeDictionary()
    {
        if (upgradeBasedOnTapioca == null)
        {
            upgradeBasedOnTapioca = new Dictionary<UpgradeInfo, Action<UpgradeInfo>>
            {
                { tapiocaCharqueReference, IncreaseHP },
                { tapiocaChickenReference, IncreaseCarryingCapacity },
                { tapiocaCoconutReference, IncreaseSpeed },
            };
        }
    }
}
