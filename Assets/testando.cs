using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testando : MonoBehaviour
{
    private ulong pontos;
    public PlayerShop shop;
    void Start()
    {
        shop = new PlayerShop();
    }
    public void Buttom()
    {
        shop.BuyButtom();
    }
}