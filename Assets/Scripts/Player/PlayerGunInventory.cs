using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunInventory : MonoBehaviour
{
    [SerializeField] public List<Gun> guns;
    [SerializeField] private ushort currentWeaponIndex;
    public static Action<ushort,Gun> onWeaponChange; 
    private ushort carryingSize;
   

    void Start()
    {
        currentWeaponIndex = 0;
        carryingSize = 2;
        EquipWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("currentWeapon: "+ guns[currentWeaponIndex]);
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            EquipWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex= 1;
            EquipWeapon();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (carryingSize >1)
            {
                currentWeaponIndex = 2;
                Debug.Log("changed index to:"+currentWeaponIndex);
                EquipWeapon();

            }
        }
    }

    public ushort GetCurrentWeaponIndex()
    {
        return this.currentWeaponIndex;
    }

    public void IncreaseCarryingSize()
    {
        carryingSize = 3; //Como so tem 3 armas equipaveis, n eh necessario botar um valor modular, ou incrementar, evitando futuros bug's na mecanica. 
    }
    public Gun GetCurrentGun()
    {
        if(guns.Count > currentWeaponIndex) return guns[currentWeaponIndex];
        return null;
    }
    public void AddNewGun(Gun gun)
    {
        guns.Add(gun);
    }

    public void EquipWeapon()
    {
        //Desativo todos
        foreach (Gun gun in guns)
        {
            if(gun == null){
              continue;
            }
            Debug.Log(gun.name);
            gun.gameObject.SetActive(false);
        }
        //Posso colocar um ~yield da animação de troca de arma aqui

        //Troca pra arma escolhida
        Debug.Log($"{guns.Count} -- {currentWeaponIndex}");
        if(guns.Count > currentWeaponIndex)
        {
            guns[currentWeaponIndex].gameObject.SetActive(true);
        }

        //Se não tiver uma arma secundária, ele fica pelado >_<
        onWeaponChange?.Invoke(currentWeaponIndex,GetCurrentGun()); 
    }
}
