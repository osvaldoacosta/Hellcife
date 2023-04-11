using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    
    [SerializeField] private KeyCode reloadKey;
    [SerializeField] private GunInfo currentGunInfo;

    private void Update()
    {
        //MELHORAR ISSO AQUI ._.
        if (currentGunInfo.isAutomatic)
        {
            if (Input.GetMouseButton(0))
            {
                shootInput?.Invoke();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootInput?.Invoke();

        }

        if (Input.GetKeyDown(reloadKey))
        {

            Debug.Log("Reloading");
            reloadInput?.Invoke();
        }
    }
}
