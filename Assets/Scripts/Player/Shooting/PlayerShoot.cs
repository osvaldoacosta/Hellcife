using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action<float> shootInput;
    public static Action reloadInput;
    [SerializeField] private float playerBaseDamage = 1;
    [SerializeField] private KeyCode reloadKey;
    [SerializeField] private GunInfo currentGunInfo;

    public void SetCurrentGunInfo(GunInfo info)
    {
        currentGunInfo = info;
    }
    

    public float GetPlayerBaseDamage()
    {
        return playerBaseDamage;
    }

    public void SetPlayerBaseDamage(float v)
    {
        playerBaseDamage= v; 
    }

    private void Update()
    {
        //MELHORAR ISSO AQUI ._.
        if (currentGunInfo.isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                shootInput?.Invoke(playerBaseDamage);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootInput?.Invoke(playerBaseDamage);

        }

        if (Input.GetKeyDown(reloadKey))
        {

            Debug.Log("Reloading");
            reloadInput?.Invoke();
        }
    }
}
