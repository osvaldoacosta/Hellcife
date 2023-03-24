using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunUpgradeShop : UpgradeShop
{
    [SerializeField] private UpgradeShopUI gunUI;
    [SerializeField] private UpgradeInfo gunDmgUpgrade;
    [SerializeField] private UpgradeInfo gunFirerateUpgrade;
    [SerializeField] private UpgradeInfo gunReloadSpeedUpgrade;
    [SerializeField] private UpgradeInfo gunCapacityUpgrade;

    private Dictionary<Gun, Dictionary<UpgradeInfo, Action<UpgradeInfo>>> upgradeBasedOnGun;
    private Gun gunToUpgrade;

    private void IncreaseGunDmg(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().damage = gunToUpgrade.GetGunInfo().damage * (float)upgrade.valueToAdd/100f;
    private void IncreaseGunFirerate(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().roundsPerMinute = gunToUpgrade.GetGunInfo().roundsPerMinute * (float)upgrade.valueToAdd/100f;
    private void ReduceGunReloadSpeed(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().reloadTime = gunToUpgrade.GetGunInfo().reloadTime * (float)upgrade.valueToAdd/100f;
    private void IncreaseGunCapacity(UpgradeInfo upgrade)
    {
        gunToUpgrade.GetGunInfo().magSize = (int)Math.Round(gunToUpgrade.GetGunInfo().magSize * (float)upgrade.valueToAdd/100);
        gunToUpgrade.GetGunInfo().currentAmmo = gunToUpgrade.GetGunInfo().magSize;
    }



    private void Awake()
    {
        List<Gun> playerGuns = GetShopInteraction().GetGunsInInventory();
        CreateGunUpgradeDictionary(playerGuns);
        //TODO: Fazer com que as armas disponiveis fiquei disponiveis na ui, e o resto fique cinzada e que a ui responda com a mudança de preços.
    }

    public void SetGunToUpgrade(Gun gun)
    {
        gunToUpgrade = gun;
    }

    public void OnGunUpgradeButtonClicked(UpgradeInfo gunUpInfo)
    {

        BuyUpgrade(gunUpInfo, upgradeBasedOnGun[gunToUpgrade]);
    }


  

    //SO IRA SER CHAMADA NO AWAKE, SE FOR CHAMADA EM OUTRO LUGAR, ALGO ESTÁ MUITO ERRADO, ~ou quem fez tá bêbo
    private void CreateGunUpgradeDictionary(List<Gun> playerGuns)
    {
        if(upgradeBasedOnGun == null)
        {
            upgradeBasedOnGun = new Dictionary<Gun, Dictionary<UpgradeInfo, Action<UpgradeInfo>>>();
        }
        
        foreach (Gun gun in playerGuns)
        {
            if (!upgradeBasedOnGun.ContainsKey(gun))
            {
                upgradeBasedOnGun.Add(gun,
                new Dictionary<UpgradeInfo, Action<UpgradeInfo>>
                {
                    {gunDmgUpgrade, IncreaseGunDmg },
                    {gunFirerateUpgrade, IncreaseGunFirerate },
                    {gunReloadSpeedUpgrade, ReduceGunReloadSpeed },
                    {gunCapacityUpgrade, IncreaseGunCapacity }
                }
                );
                CreateReferenceToInstantiatedDictionary(new HashSet<UpgradeInfo>(upgradeBasedOnGun[gun].Keys));
            }


        }
    }
}
