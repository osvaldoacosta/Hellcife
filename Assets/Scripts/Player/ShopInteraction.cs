
using System.Collections.Generic;
using UnityEngine;

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


    public void IncreaseHpWithTapioca(ushort hpToAdd)
    {
        Target player = GetComponent<Target>();
        float newHp = player.GetMaxHealth() + hpToAdd;
        player.SetMaxHealth(newHp);
        player.Heal(hpToAdd); //So vai curar oque adicionou a mais

    }
    public void IncreaseSpeedWithTapioca(ushort speedPercentageToAdd)
    {
        PlayerMovement player = GetComponent<PlayerMovement>();
        float value = player.movementSpeed * (float)speedPercentageToAdd / 100f;
        player.SetSpeed(value); //Como valueToAdd da tapiocainfo ta com um ushort, eh melhor usar porcentagem do que fazer uma conversao (ushort, float) desnecessaria
    }
    public void IncreaseCarryingCapacityWithTapioca()
    {
        GetComponent<PlayerGunInventory>().IncreaseCarryingSize();
    }

   


}
