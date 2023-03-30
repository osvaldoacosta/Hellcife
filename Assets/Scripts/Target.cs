using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] private float current_health;
    private float max_health;

    void Start()
    {
        max_health = 100;
    }
    public float GetHealth()
    {
        return current_health;
    }
    public void SetMaxHealth(float health)
    {
        this.max_health = health;
    }
    public void TakeDamage(float damage)
    {
        current_health -= damage;
        IsDead();
        Debug.Log("target hp: "+current_health);
        
    }
    
    //Usado para qd tiver curas por exemplo
    public void Heal(float heal)
    {
        if(heal+current_health <= max_health)
        {
            current_health += heal;
        }
    }
    public bool IsDead()
    {
        if (current_health <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }


}
