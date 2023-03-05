using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [Header("Reference")]
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private GunInfo gunInfo;
    [SerializeField] private Transform muzzle; //Precisa criar um objeto vazio e botar na boca do cano da arma(se a arma n�o vier com um objeto muzzle)
    [SerializeField] private Transform bulletProjectile; //Objeto da balita
    private float timeSinceLastShot;

    void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        PlayerLook.aimingInput += Aim;
        timeSinceLastShot = 0f;
    }

    public GunInfo GetGunInfo()
    {
        return gunInfo;
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
            if (gunInfo.isMagReloaded)
            {
                yield return new WaitForSeconds(gunInfo.reloadTime); //Espera o reload acabar
                gunInfo.currentAmmo = gunInfo.magSize;
            }
            else
            {
                int bulletsToReload = gunInfo.magSize - gunInfo.currentAmmo;
                float timeToPutOneAmmo = gunInfo.reloadTime / gunInfo.magSize;
                for (int i = 0; i < bulletsToReload; i++)
                {
                    yield return new WaitForSeconds(timeToPutOneAmmo);
                    gunInfo.currentAmmo = gunInfo.currentAmmo + 1;
                }
            }
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
        //Transform bullet = Instantiate(bulletProjectile, muzzle.position,Quaternion.LookRotation(muzzleDirection)); //Usando o m�todo tradicional de instanciar uma bala nova
        //Debug.DrawRay(bullet.position, muzzleDirection*10,Color.blue);
        //Usando um game object pool, com as balas

        GameObject bullet = bulletPool.GetPooledObject();

        bullet.GetComponent<Bullet>().SetDamage(gunInfo.damage); //Seta o dano dessa balita
        bullet.transform.position = muzzle.position; //Bota a bala na posi��o certa
        
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * 30f; //Fazer algum calculo doido para velocidade da bala
        bullet.SetActive(true);
        
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
