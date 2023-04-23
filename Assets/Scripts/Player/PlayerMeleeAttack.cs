using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] SwordAttackHitbox swordAttackHitbox;
    [SerializeField] KeyCode meleeAttackKey;
    [SerializeField] float meleeAttackCooldown = 5f;
    float endOfMeleeAttackCooldown = -1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < endOfMeleeAttackCooldown) return;
        if(Input.GetKeyDown(meleeAttackKey)){
            endOfMeleeAttackCooldown= Time.time + meleeAttackCooldown;
            swordAttackHitbox.checkHitAndDealDamage(100, 0.1f);
        }
    }
}
