using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints: MonoBehaviour
{
    private ulong points;
    void Start()
    {
        points= 0;
    }

    public void GainPoints(ulong points)
    {
        this.points += points; 
    }
    public void LosePoints(ulong points)
    {
        this.points -= points;
    }

    public ulong GetPoints()
    {
        return points;
    }
    

}
