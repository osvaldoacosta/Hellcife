
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//classes com essa interface podem levar dano (player,inimigo, etc)
public interface IDamageable
{
    public void TakeDamage(float damage);
    public float GetHealth();
}
