using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private PlayerPoints playerPoints;
    
    private void Awake()
    {
        playerPoints = GetComponent<PlayerPoints>();
       

    }

    public bool CanPlayerBuy(uint price) => playerPoints.GetPoints() >= price;



}
