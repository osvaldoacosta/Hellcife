using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunInventory : MonoBehaviour
{
    [SerializeField]public List<Gun> guns;

    [SerializeField] public ushort currentWeaponIndex;
    private ushort carryingSize;
    

    public ushort GetCurrentWeaponIndex()
    {
        return this.currentWeaponIndex;
    }

    public void IncreaseCarryingSize()
    {
        carryingSize++;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        carryingSize = 2;
    }

    // Update is called once per frame
    void Update()
    {
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
                EquipWeapon();

            }
        }
    }

    public void AddNewGun(Gun gun)
    {
        guns.Add(gun);
    }

    public void EquipWeapon()
    {
        Debug.Log(guns.Count);
        //Desativo todos
        foreach (Gun gun in guns)
        {
            Debug.Log(gun.name);
            gun.gameObject.SetActive(false);
        }
        //Posso colocar um ~yield da animação de troca de arma aqui

        //Troca pra arma escolhida
        if(guns.Count > currentWeaponIndex)
        {
            guns[currentWeaponIndex].gameObject.SetActive(true);
        }
        //Se não tiver uma arma secundária, ele fica pelado >_<
        
    }
}
