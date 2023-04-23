using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Gun[] guns;
    

    void Start()
    {
        gameObject.tag = "Player";
        //Vida
        //gameObject.GetComponent<Target>().SetMaxHealth(100f);
        //Arma inicial

        gameObject.GetComponent<PlayerGunInventory>().guns.Add(guns[0]);
        // gameObject.GetComponent<PlayerHudController>().InitialWeaponChange(gun.GetGunInfo()); 
        foreach(Gun gun in guns)
        {
            GunInfo guninfo = gun.GetGunInfo();
            guninfo.isReloading = guninfo.isReloading && false;
            gun.SetGunInfo(Instantiate(guninfo));
        }
        


    }


}
