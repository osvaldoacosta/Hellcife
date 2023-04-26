using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRb;
    private float damage;
    
    private void Awake()
    {
        bulletRb = GetComponent<Rigidbody>();
    }

   
    private void OnEnable()
    {
        this.gameObject.GetComponent<TrailRenderer>().Clear();
        Invoke("Disable", 5f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>(); //checa se o objeto que colidiu possui a interface do IDamageable
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            PlayerPoints points = GameObject.FindWithTag("Player").GetComponent<PlayerPoints>(); //Utilizar um jeito melhor dps, referenciar o player pra cada bala ficar� pesado ._.
            if (points != null)
            {
                points.GainPoints(15); //Setar l�gica de como vai ganhar diferentes pontos
                Debug.Log("Player points: " + points.GetPoints());
            }
            
        }
        if(other.GetComponent<Bullet>() == null) //Se nao tocar em nada alem de outras balas e inimigos, vai desabilitar a bala
        { 
            Disable();
        }
    }
    private void Disable()
    {
        
        bulletRb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
