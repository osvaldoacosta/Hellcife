using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Essa classe sera util para o hud e para a loja de upgrades.
public class PlayerPowerUpInventory : MonoBehaviour
{
    public Dictionary<string, ushort> powerUps; //Power up e sua quantidade

    private void Awake()
    {
        powerUps = new Dictionary<string, ushort>();
    }
    
    //Logica do limite de power ups no script da loja O_O.
    public void AddPowerUp(string name, ushort qty)
    {
        if (!powerUps.TryAdd(name, qty)) //da falso se a key estiver no dictionary.
        {
            powerUps[name] = (ushort)(powerUps[name] + qty);
        }
    }

    
}
