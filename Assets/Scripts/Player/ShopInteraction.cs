
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Playables;

//Ao player comprar um item na loja, essa classe eh chamada para modificar status do player.
public class ShopInteraction : MonoBehaviour
{
    private PlayerGunInventory gunInventory;

    public List<Gun> GetGunsInInventory()
    {
        return gunInventory.guns;
    }

    public ushort GetInventoryCapacity()
    {
        return gunInventory.GetCarringSize();
    }

    private void Awake()
    {
        gunInventory = GetComponent<PlayerGunInventory>();
    }


    public void IncreasePlayerHp(ushort hpToAdd)
    {
        Target player = GetComponent<Target>();
        float newHp = player.GetMaxHealth() + hpToAdd;
        player.SetMaxHealth(newHp);
        player.Heal(hpToAdd); //So vai curar oque adicionou a mais

    }
    public void IncreasePlayerSpeed(ushort speedPercentageToAdd)
    {
        PlayerMovement player = GetComponent<PlayerMovement>();
        float value = player.movementSpeed * (float)speedPercentageToAdd / 100f;
        player.SetSpeed(value); //Como valueToAdd da tapiocainfo ta com um ushort, eh melhor usar porcentagem do que fazer uma conversao (ushort, float) desnecessaria
    }
    public void IncreaseCarryingCapacity()
    {
        GetComponent<PlayerGunInventory>().IncreaseCarryingSize();
    }

   
    public void HealPlayer(ushort hpToHeal)
    {
        Target player = GetComponent<Target>();
        player.Heal(hpToHeal);
    }

    public IEnumerator UseItemWithEffect(ushort healIncrementPerc, ushort damageIncrementPerc, ushort speedIncrementPerc, float effectDuration)
    {
        Debug.Log("Entrou");
        Target player = GetComponent<Target>();
        PlayerShoot playerShoot = GetComponent<PlayerShoot>();
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        float oldPlayerSpeed = playerMovement.movementSpeed;
        float oldPlayerMaxHp = player.GetMaxHealth();
        float oldPlayerDamage = playerShoot.GetPlayerBaseDamage();

        if (healIncrementPerc > 100)
        {
            float newMaxHp = player.GetMaxHealth() * healIncrementPerc/100f;
            player.SetMaxHealth(newMaxHp);
            player.Heal(newMaxHp - oldPlayerMaxHp);
        }
        if (damageIncrementPerc > 100)
        {
            playerShoot.SetPlayerBaseDamage(damageIncrementPerc/100f * oldPlayerDamage   );
        }
        if(speedIncrementPerc > 100)
        {
            IncreasePlayerSpeed(speedIncrementPerc);
        }

        
        try
        {
            yield return new WaitForSeconds(effectDuration);
        }
        finally
        {
            player.SetMaxHealth(oldPlayerMaxHp);
            player.Heal(0); //Capa o hp atual na vida maxima
            playerShoot.SetPlayerBaseDamage(oldPlayerDamage);
            playerMovement.SetSpeed(oldPlayerSpeed);
        }

    }
}
