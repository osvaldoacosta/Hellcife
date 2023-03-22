using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : Shop
{
    [SerializeField] private TapiocaInfo tapiocaCharque;
    [SerializeField] private TapiocaInfo tapiocaChicken;
    [SerializeField] private TapiocaInfo tapiocaCoconut;

    private Dictionary<TapiocaInfo, Action<TapiocaInfo>> tapiocaMethods;


    private void IncreaseHP(TapiocaInfo tapioca) => GetShopInteraction().IncreaseHpWithTapioca(tapioca.valueToAdd);
    private void IncreaseSpeed(TapiocaInfo tapioca) => GetShopInteraction().IncreaseSpeedWithTapioca(tapioca.valueToAdd);
    private void IncreaseCarryingCapacity(TapiocaInfo tapioca) => GetShopInteraction().IncreaseCarryingCapacityWithTapioca();
    private void Awake()
    {
        tapiocaMethods = new Dictionary<TapiocaInfo, Action<TapiocaInfo>>
        {
            { tapiocaCharque, IncreaseHP },
            { tapiocaChicken, IncreaseCarryingCapacity },
            { tapiocaCoconut, IncreaseSpeed }
        };
    }

    
    public Dictionary<TapiocaInfo, Action<TapiocaInfo>> GetTapiocaMethods() => tapiocaMethods;



}
