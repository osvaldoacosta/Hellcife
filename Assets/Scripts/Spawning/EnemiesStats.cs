using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesStats : MonoBehaviour
{
    public float defaultMeleeEnemyHealth;
    public float defaultMeleeEnemySpeed;

    public float defaultGooEnemyHealth;
    public float defaultGooEnemySpeed;

    public float defaultExplosiveEnemyHealth;
    public float defaultExplosiveEnemySpeed;

    public float defaultMeleeBossEnemyHealth;
    public float defaultMeleeBossEnemySpeed;

    public float defaultGooBossEnemyHealth;
    public float defaultGooBossEnemySpeed;

    public float defaultExplosiveBossEnemyHealth;
    public float defaultExplosiveBossEnemySpeed;

    public void Start(){
        defaultMeleeEnemyHealth = 100f;
        defaultMeleeEnemySpeed = 5.5f;

        defaultGooEnemyHealth = 75f;
        defaultGooEnemySpeed = 2f;

        defaultExplosiveEnemyHealth= 150f;
        defaultExplosiveEnemySpeed= 4.5f;

        defaultMeleeBossEnemyHealth= 1000f;
        defaultMeleeBossEnemySpeed= 5f;

        defaultGooBossEnemyHealth= 800f;
        defaultGooBossEnemySpeed= 2f;

        defaultExplosiveBossEnemyHealth= 1200f;
        defaultExplosiveBossEnemySpeed= 4.5f;
    }
}
