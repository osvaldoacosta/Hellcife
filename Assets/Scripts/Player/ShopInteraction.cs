using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;

//Ao player comprar um item na loja, essa classe eh chamada para modificar status do player.
public class ShopInteraction : MonoBehaviour
{
    private PlayerPoints pp;

    private void Awake()
    {
        pp = GetComponent<PlayerPoints>();
    }

    //Nao eh necessario checar se eh possivel gastar os pontos, logica de checagem no script da loja. O_o
    private void SpendPoints(ushort price)
    {
        pp.LosePoints(price);
    }
    public void IncreaseHpWithTapioca(ushort hpToAdd, ushort price)
    {
        SpendPoints(price);
        Target player = GetComponent<Target>(); 
        player.SetMaxHealth(player.GetMaxHealth() + hpToAdd);
    }
    public void IncreaseSpeedWithTapioca(ushort speedPercentageToAdd, ushort price)
    {
        SpendPoints(price);
        PlayerMovement player = GetComponent<PlayerMovement>();
        player.movementSpeed = player.movementSpeed * speedPercentageToAdd; //Como valueToAdd da tapiocainfo ta com um ushort, eh melhor usar porcentagem do que fazer uma conversao (ushort, float) desnecessaria
    }
    public void IncreaseCarryingCapacityWithTapioca(ushort price)
    {
        SpendPoints(price);
        GetComponent<PlayerGunInventory>().IncreaseCarryingSize();
    }
    

}
