using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cheatzinhos bem xoxos UwU xD
public class Cheats4Debug : MonoBehaviour
{
    private PlayerHudController hud;
    private PlayerGunInventory gunIn;

    //Como no momento não possuo o script da loja nessa branch, ta aq as referencias pras armas do player
    [SerializeField] private Gun pistol;
    [SerializeField] private Gun carabine;
    [SerializeField] private Gun shotgun;

    void Awake()
    {
        hud = GetComponent<PlayerHudController>();
        gunIn= GetComponent<PlayerGunInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hud.ExtendInventoryHud();
        }
        else if(Input.GetKeyDown(KeyCode.G))
        {
            //Ganha todas as armas
            gunIn.IncreaseCarryingSize();
            gunIn.AddNewGun(carabine);
            gunIn.AddNewGun(shotgun);
        }
    }
}
