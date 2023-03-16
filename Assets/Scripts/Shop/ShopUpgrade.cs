using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUpgrade : MonoBehaviour
{
    public Dictionary<TapiocaInfo, uint> tapiocas; // (nomeTapioca, preco).
    private PlayerPoints playerPoints;
    private PlayerGunInventory gunInventory;
    private PlayerPowerUpInventory powerUpInventory;

    /*
Upgrade de arma
-  Liberar a opção do player melhorar estatísticas da sua arma atual com um preço
-  Essas melhorias são únicas, e custam um preço ao player, ao serem compradas, a sua venda é bloqueada.

Upgrade único de arma
-  Caso o player escolha um upgrade, o mesmo  deve passar para a sua próxima versão( I, II, III) com um preço maior.
-  Ao chegar no upgrade III encerrar a venda de upgrades unicos para a arma atual.

Power-ups
-  Para os power ups(tapiocas) ao chegar no limite máximo de um mesmo tipo, bloqueie evoluções futuras desse tipo de power-up.
Ex: Vida máxima possível: 1000, ao comprar o power-up de vida, e o player ficar com 1000 de vida, a loja deve encerrar a venda desse power-up.
     
     */
    // Start is called before the first frame update
    private void Awake()
    {
        playerPoints= GetComponent<PlayerPoints>();
        gunInventory= GetComponent<PlayerGunInventory>();
        powerUpInventory= GetComponent<PlayerPowerUpInventory>();
    }

    private bool IsPowerUpAvaliable(TapiocaInfo tapioca)
    {
	    if(tapioca.useLimit > 0){
            return true;
        }
        return false;
    }

    private bool CanPlayerBuyPowerUp(TapiocaInfo tapioca)
    {
        if(playerPoints.GetPoints() >= tapiocas[tapioca])
        {
            return true;
        }
        return false;
    }

    public void BuyPowerUp()
    {

    }

    public void BuyGunUpgrade()
    {

    }

}
