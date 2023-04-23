using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunShop : BuyShop
{
    private Gun selectedGun;
    [SerializeField] private Button gunButton;
    [SerializeField] private GameObject buyShop;
    private BuyableGun buyableGun;
    private void OnEnable()
    {
        GreyOutWeapons();
        

    }

    public void BuyWeapon(BuyableGun buyableGun)
    {
        this.buyableGun = buyableGun;

        List<Gun> guns= GetShopInteraction().GetPlayerGunInInventory();
        /*
        foreach(Gun gun in guns)
        {
            if (gun.Equals(selectedGun) || !buyableGun.isAvaliable)
            {
                gunButton.interactable = false;
                return;
            }
             gunButton.interactable = true;
        }
        */
        CheckForInventorySpace(selectedGun, guns, GetShopInteraction().GetInventoryCapacity());
    }
    private void CheckForInventorySpace(Gun gun, List<Gun> inventoryGuns, ushort capacity)
    {
        if(inventoryGuns.Count < capacity)
        {
            Buy(buyableGun);
            inventoryGuns.Add(gun);

        }
        else
        {
            buyShop.GetComponent<GunShopUI>().OpenDecisionPopUp();
        }
    }

   
    public void ChangeWeaponSlot(bool isFirstSlot)
    {
        List<Gun> guns = GetShopInteraction().GetPlayerGunInInventory();
        if(isFirstSlot)
        {
            guns.RemoveAt(0);
            guns.Insert(0, selectedGun);
            
        }
        else
        {
            guns.RemoveAt(1);
            guns.Insert(1, selectedGun);
        }
        Buy(buyableGun);
    }



    private void GreyOutWeapons()
    {
        
    }
    public void SetGun(Gun gun)
    {
        selectedGun= gun;
    }
    public void SetButton(Button button)
    {
        gunButton = button;
    }
}
