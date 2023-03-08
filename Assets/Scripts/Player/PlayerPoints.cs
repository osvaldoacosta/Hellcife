using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints: MonoBehaviour
{
    private ulong points;
    void Start()
    {
        points= 100;
    }

    public void GainPoints(ulong points)
    {
        this.points += points; 
    }

    public void SetPoints()
    {
        Debug.Log("ponto = " + points);
    }

    public void RemovePoints(ulong points)
    {
        this.points -= points;
    } 

    public ulong GetPoints()
    {
        return points;
    }

}
