using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    int damage;
    float timeToDeactivate= -1f;
    bool isActive= false;

    public void Awake(){
        gameObject.SetActive(false);
    }
    public void checkHitAndDealDamage(int damage, float activeTime){
        gameObject.SetActive(true);
        this.damage = damage;

        timeToDeactivate= Time.time + activeTime;
    }
    public void Update(){
        if(Time.time > timeToDeactivate){
            isActive= false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isActive){
            if(other.gameObject.tag != "Player") return;
            IDamageable damageable = other.GetComponent<IDamageable>(); //checa se o objeto que colidiu possui a interface do IDamageable
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                isActive= false;
                Debug.Log("player was hit for "+damage+"!");
            }
        }
    }
    private void OnTriggerStay(Collider other){
        OnTriggerEnter(other);
    }
}
