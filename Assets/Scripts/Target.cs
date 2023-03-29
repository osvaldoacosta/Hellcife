using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    [SerializeField] string bloodType;
    [SerializeField] ObjectPool bloodParticlePool;
    private EphemeralParticle ephemeralParticle;
    [SerializeField] private bool bleeds;
    [SerializeField] private float current_health;
    private float max_health;

    void Start()
    {
        switch (bloodType)
        {
            case "red":
                bloodParticlePool = GameObject.FindWithTag("BloodParticlePool").GetComponent<ObjectPool>();
                break;
            case "goo":
                bloodParticlePool = GameObject.FindWithTag("GooParticlePool").GetComponent<ObjectPool>();
                break;
            default:
                bloodParticlePool = GameObject.FindWithTag("BloodParticlePool").GetComponent<ObjectPool>();
                break;
        }
        bleeds= true;
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
    //dano sem particula
    public void TakeDamage(float damage)
    {
        ephemeralParticle = bloodParticlePool.GetPooledObject().GetComponent<EphemeralParticle>();
        if(ephemeralParticle!=null){
            ephemeralParticle.transform.position= transform.position;
            ephemeralParticle.Emit(5);
        }
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
