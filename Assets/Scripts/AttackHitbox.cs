using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    int damage;
    float timeToDeactivate= -1f;
    public bool isActive= false;

    public void Awake(){
        gameObject.SetActive(false);
    }
    public void checkHitAndDealDamage(int damage, float activeTime){
        gameObject.SetActive(true);
        isActive= true;
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
        Debug.Log("HIT");
        if(isActive){
            Debug.Log("HIT ACTIVE");
            if(other.gameObject.tag != "Player") return;
            Debug.Log("HIT PLAYER!");
            IDamageable damageable = other.GetComponent<IDamageable>(); //checa se o objeto que colidiu possui a interface do IDamageable
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log("PLAYER TOOK DAMAGE");
                isActive= false;
                Debug.Log("player was hit for "+damage+"!");
            }
        }
    }
    private void OnTriggerStay(Collider other){
        OnTriggerEnter(other);
    }
}
