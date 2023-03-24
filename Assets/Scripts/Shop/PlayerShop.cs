using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    private ulong pontos;
    private PlayerPoints points;
    public GameObject glock0;
    public GameObject glock1;
    public GameObject glock2;
    private PlayerGunInventory listgun;
    public GameObject EquipAmmo0;
    public GameObject EquipAmmo1;
    public GameObject EquipAmmo2;
    public Button BuyAmmo0;
    public Button BuyAmmo1;
    public Button BuyAmmo2;
    public GameObject buy;
    void Start()
    {
        points = gameObject.GetComponent<PlayerPoints>();
        listgun = gameObject.GetComponent<PlayerGunInventory>();
        BuyAmmo0.interactable = false;
    }

    public void BuyGlock0Buttom()
    {
        Gun gun0 = glock0.GetComponent<Gun>();
        if (points.GetPoints() >= 5)
        {
            points.LosePoints(5);
            if (listgun.guns.Count == 1)
            if (listgun.guns.Count == 1)
            {
                listgun.guns.Add(gun0);
                BuyAmmo0.interactable = false;
            }
            else
            {
                buy.SetActive(false);
                EquipAmmo0.SetActive(true);
            }
        }
    }

    public void Ammo(Button buyAmmo, int index)
    {
        buyAmmo.interactable = false;
        /*EnableAmmo(listgun.guns[index]);
        listgun.guns.RemoveAt(index);
        listgun.guns.Insert(index, gun);*/
        index = 1;
    }

    public void EquipGlock0Slot1(Gun gun)
    {
        Ammo(BuyAmmo0, 0);
    }

    public void EquipGlock0Slot2(Gun gun)
    {
        Ammo(BuyAmmo0, 1);
    }

    public void BuyGlock1Buttom()
    {
        Gun gun1 = glock1.GetComponent<Gun>();
        if (points.GetPoints() >= 5)
        {
            points.LosePoints(5);
            if (listgun.guns.Count == 1)
            {
                listgun.guns.Add(gun1);
                BuyAmmo1.interactable = false;
            }
            else
            {
                buy.SetActive(false);
                EquipAmmo1.SetActive(true);
            }
        }
    }

    public void EquipGlock1Slot1(Gun gun)
    {
        BuyAmmo1.interactable = false;
        EnableAmmo(listgun.guns[0]);
        listgun.guns.RemoveAt(0);
        listgun.guns.Insert(0, gun);
    }

    public void EquipGlock1Slot2(Gun gun)
    {
        BuyAmmo1.interactable = false;
        EnableAmmo(listgun.guns[1]);
        listgun.guns.RemoveAt(1);
        listgun.guns.Insert(1, gun);
    }

    public void BuyGlock2Buttom()
    {
        Gun gun2 = glock2.GetComponent<Gun>();
        if (points.GetPoints() >= 5)
        {
            points.LosePoints(5);
            if (listgun.guns.Count == 1)
            {
                BuyAmmo2.interactable = false;
                listgun.guns.Add(gun2);
            }
            else
            {
                buy.SetActive(false);
                EquipAmmo2.SetActive(true);
            }
        }
    }

    public void EquipGlock2Slot1(Gun gun)
    {
        BuyAmmo2.interactable = false;
        EnableAmmo(listgun.guns[0]);
        listgun.guns.RemoveAt(0);
        listgun.guns.Insert(0, gun);
    }

    public void EquipGlock2Slot2(Gun gun)
    {
        BuyAmmo2.interactable = false;
        EnableAmmo(listgun.guns[1]);
        listgun.guns.RemoveAt(1);
        listgun.guns.Insert(1, gun);
    }

    public void EnableAmmo(Gun gun)
    {

        if (gun.name == "glock_fix")
        {
            BuyAmmo0.interactable = true;
            glock0.SetActive(false);
        }
        else if (gun.name == "glock_fixsaaaa")
        {
            BuyAmmo1.interactable = true;
            glock1.SetActive(false);
        }
        else if (gun.name == "glock_fixkkkk")
        {
            BuyAmmo2.interactable = true;
            glock2.SetActive(false);
        }
    }
}
