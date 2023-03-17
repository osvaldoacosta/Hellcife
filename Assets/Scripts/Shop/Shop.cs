using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe base da loja
public class Shop : MonoBehaviour
{
    
    private PlayerPoints playerPoints;
    
    private void Awake()
    {
        playerPoints = GetComponent<PlayerPoints>();
    }

    public bool CanPlayerBuy(uint price) => playerPoints.GetPoints() >= price;

    public void LosePlayerPointsWhenBuying(uint price) => playerPoints.LosePoints(price);

    
}
