using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField] private PlayerPoints playerPoints;
    [SerializeField] private ShopInteraction shopInteraction;

    public bool CanPlayerBuy(uint price) => playerPoints.GetPoints() >= price;
       
    public void SpendPlayerPointsOnBuy(uint price) => playerPoints.LosePoints(price);

    public ShopInteraction GetShopInteraction() => shopInteraction;
}
