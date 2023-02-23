using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject defaultWeapon;
    void Start()
    {
        gameObject.tag = "Player";
        //Vida
        //gameObject.GetComponent<Target>().SetMaxHealth(100f);
        //Arma inicial
        Gun gun = defaultWeapon.GetComponent<Gun>();

        gameObject.GetComponent<PlayerGunInventory>().guns.Add(gun);
        // gameObject.GetComponent<PlayerHudController>().InitialWeaponChange(gun.GetGunInfo()); 
        //Qtd de Itens
        
        
        //EtC
    }


}
