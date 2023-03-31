using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunUpgradeShop : UpgradeShop
{
    [SerializeField] private UpgradeShopUI gunUI;
    [SerializeField] private UpgradeInfo gunDmgUpgradeRef;
    [SerializeField] private UpgradeInfo gunFirerateUpgradeRef;
    [SerializeField] private UpgradeInfo gunReloadSpeedUpgradeRef;
    [SerializeField] private UpgradeInfo gunCapacityUpgradeRef;
    [SerializeField] private UpgradeInfo uniqueGunUpgradeRef;

    [SerializeField] private Gun pistolReference;
    [SerializeField] private Gun shotgunReference;
    [SerializeField] private Gun rifleReference;

    private Gun gunToUpgrade;

    private Dictionary<Gun, Dictionary<UpgradeInfo, (Action<UpgradeInfo>, UpgradeInfo)>> upgradeBasedOnGun;

    private void IncreaseGunDmg(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().damage = gunToUpgrade.GetGunInfo().damage * (float)upgrade.valueToAdd/100f;
    private void IncreaseGunFirerate(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().roundsPerMinute = gunToUpgrade.GetGunInfo().roundsPerMinute * (float)upgrade.valueToAdd/100f;
    private void ReduceGunReloadSpeed(UpgradeInfo upgrade) => gunToUpgrade.GetGunInfo().reloadTime = gunToUpgrade.GetGunInfo().reloadTime * (float)upgrade.valueToAdd/100f;
    private void IncreaseGunCapacity(UpgradeInfo upgrade)
    {
        gunToUpgrade.GetGunInfo().magSize = (int)Math.Round(gunToUpgrade.GetGunInfo().magSize * (float)upgrade.valueToAdd/100);
        gunToUpgrade.GetGunInfo().currentAmmo = gunToUpgrade.GetGunInfo().magSize;
    }

 

    private void OnEnable()
    {
        List<Gun> playerGuns = GetShopInteraction().GetPlayerGunInInventory();
        CreateGunUpgradeDictionary(playerGuns);
    }

    public void SetGunToUpgrade(Gun gun)
    {
        gunToUpgrade = gun;
    }

    public void OnGunUpgradeButtonClicked(UpgradeInfo gunUpInfo)
    {

        BuyUpgrade(gunUpInfo, upgradeBasedOnGun[gunToUpgrade]);
    }


  

    //SO IRA SER CHAMADA NO OnEnable, SE FOR CHAMADA EM OUTRO LUGAR, ALGO ESTÁ MUITO ERRADO, ~ou quem fez tá bêbo
    private void CreateGunUpgradeDictionary(List<Gun> playerGuns)
    {
        if(upgradeBasedOnGun == null)
        {
            upgradeBasedOnGun = new Dictionary<Gun, Dictionary<UpgradeInfo, (Action<UpgradeInfo>, UpgradeInfo)>>();
        }
        
        foreach (Gun gun in playerGuns)
        {
            if (!upgradeBasedOnGun.ContainsKey(gun))
            {
                upgradeBasedOnGun.Add(gun,
                new Dictionary<UpgradeInfo, (Action<UpgradeInfo>, UpgradeInfo)>
                {
                    {gunDmgUpgradeRef, (IncreaseGunDmg,Instantiate(gunDmgUpgradeRef))},
                    {gunFirerateUpgradeRef, (IncreaseGunFirerate, Instantiate(gunFirerateUpgradeRef)) },
                    {gunReloadSpeedUpgradeRef, (ReduceGunReloadSpeed, Instantiate(gunReloadSpeedUpgradeRef)) },
                    {gunCapacityUpgradeRef, (IncreaseGunCapacity, Instantiate(gunCapacityUpgradeRef)) },
                    {uniqueGunUpgradeRef, (RealizeUniqueUpgradeOnGun, Instantiate(uniqueGunUpgradeRef)) },
                }
                );
            }


        }
    }


    private void RealizeUniqueUpgradeOnGun(UpgradeInfo info)
    {
        ushort upgradeNumber = (ushort)(4 - info.useLimit);
        GunInfo gunInfo = gunToUpgrade.GetGunInfo();

        switch (upgradeNumber)
        {
            case 1:
                if (gunToUpgrade.Equals(pistolReference))
                {
                    Debug.Log("Upgrade unico da pistola 1");
                    gunInfo.damage = gunInfo.damage * 2;
                    gunInfo.magSize = gunInfo.magSize * 2;
                    gunInfo.currentAmmo = gunInfo.magSize;
                }
                else if (gunToUpgrade.Equals(shotgunReference))
                {
                    Debug.Log("Upgrade unico da shotgun 1");
                    gunInfo.bulletsPerShot = (ushort)(gunInfo.bulletsPerShot * 2);
                    gunInfo.damage = gunInfo.damage * 1.5f;
                }
                else if (gunToUpgrade.Equals(rifleReference))
                {
                    Debug.Log("Upgrade unico da carabina 1");
                    gunInfo.magSize = gunInfo.magSize * 2;
                    gunInfo.currentAmmo = gunInfo.magSize;
                    gunInfo.damage = gunInfo.damage * 1.5f;
                }


                break;
            case 2:

                if (gunToUpgrade.Equals(pistolReference))
                {
                    Debug.Log("Upgrade unico da pistola 2");
                    gunInfo.magSize = gunInfo.magSize * 2;
                    gunInfo.currentAmmo = gunInfo.magSize;
                    gunInfo.bulletsPerShot = 2;
                }
                else if (gunToUpgrade.Equals(shotgunReference))
                {
                    Debug.Log("Upgrade unico da shotgun 2");

                    gunInfo.reloadTime = gunInfo.reloadTime * 0.5f;
                    gunInfo.maxDistance = gunInfo.maxDistance * 2;
                    gunInfo.roundsPerMinute = gunInfo.roundsPerMinute * 2f;

                }

                else if (gunToUpgrade.Equals(rifleReference))
                {
                    Debug.Log("Upgrade unico da carabina 2");
                    gunInfo.magSize = gunInfo.magSize * 2;
                    gunInfo.currentAmmo = gunInfo.magSize;
                    gunInfo.bulletsPerShot = 2;
                }
                break;
            case 3:
                if (gunToUpgrade.Equals(pistolReference))
                {
                    Debug.Log("Upgrade unico da pistola 3");
                    gunInfo.isAutomatic = true;
                    Gun secondGun = Instantiate(gunToUpgrade);
                    GetShopInteraction().GetPlayerGunInInventory().Add(secondGun);

                }
                else if (gunToUpgrade.Equals(shotgunReference))
                {
                    Debug.Log("Upgrade unico da shotgun 3");
                    gunInfo.magSize = 10;
                    gunInfo.currentAmmo = gunInfo.magSize;
                    gunInfo.bulletsPerShot = 1;
                    gunInfo.damage = gunInfo.damage * 5;

                }

                else if (gunToUpgrade.Equals(rifleReference))
                {
                    Debug.Log("Upgrade unico da carabina 3");
                    gunInfo.magSize = gunInfo.magSize * 2;
                    gunInfo.isAutomatic = true;
                }

                break;

            default:
                break;
        }
    }


}
