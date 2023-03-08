using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShop : MonoBehaviour
{
    private ulong pontos;
    private PlayerPoints points;
    public GameObject glock;
    void Start()
    {
        points = gameObject.GetComponent<PlayerPoints>();
    }
    public void BuyButtom()
    {
        if(points.GetPoints() >= 5)
        {
            points.RemovePoints(5);
        }
    }
}
