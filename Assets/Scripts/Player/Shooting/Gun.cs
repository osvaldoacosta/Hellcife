using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private GunInfo gunInfo;
    [SerializeField] private Transform muzzle; //Precisa criar um objeto vazio e botar na boca do cano da arma(se a arma não vier com um objeto muzzle)
    [SerializeField] private Transform bulletProjectile; //Objeto da balita
    float timeSinceLastShot;
    

    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        PlayerLook.aimingInput += Aim;
        timeSinceLastShot = 0f;
    }
    private void Aim(bool isAiming) => gunInfo.isAiming = isAiming;

    private bool CanShoot() => !gunInfo.isReloading && timeSinceLastShot > 1f / (gunInfo.roundsPerMinute/60) && gunInfo.isAiming ;

    private void StartReload()
    {
        if (!gunInfo.isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        if (gunInfo.isReloading == false)
        {
            gunInfo.isReloading = true;
            yield return new WaitForSeconds(gunInfo.reloadTime); //Espera o reload acabar

            gunInfo.currentAmmo = gunInfo.magSize;
            gunInfo.isReloading = false;
        }

    }
    private void Shoot()
    {
        if (gunInfo.currentAmmo > 0)
        {
            //Debug.Log(gunInfo.isAiming + ", isReloading:" + gunInfo.isReloading + " - " + CanShoot());

            if (CanShoot())
            {
                OnGunShoot();
            }
        }
    }

    private void OnGunShoot()
    {

        Vector3 muzzleDirection = muzzle.transform.TransformDirection(Vector3.forward);
        Transform bullet = Instantiate(bulletProjectile, muzzle.position,Quaternion.LookRotation(muzzleDirection));
        //Debug.DrawRay(bullet.position, muzzleDirection*10,Color.blue);

        
        gunInfo.currentAmmo -= 1;
        timeSinceLastShot = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Vector3 dir = muzzle.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(muzzle.position, dir * 10, Color.blue);
    }
}
