using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject); //Quando for fazer o object pool dos inimigos, desativar inves de destruir
        }
    }
    
    //Usado para qd tiver curas por exemplo
    public void Heal(float heal)
    {
        health += heal;
    }
}
