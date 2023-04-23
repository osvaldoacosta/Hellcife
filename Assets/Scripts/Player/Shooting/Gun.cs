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
    [SerializeField] private PlayerRiggingModifier playerRigging;
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

    public void SetGunInfo(GunInfo gunInfo)
    {
        this.gunInfo = gunInfo;
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
                //Para cada bala da shotgun ele vai dar o yield
                for (int i = 0; i < bulletsToReload; i++)
                {
                    yield return new WaitForSeconds(timeToPutOneAmmo);
                    gunInfo.currentAmmo = gunInfo.currentAmmo + 1;
                }
            }
            gunInfo.isReloading = false;
                
        }

    }
    private void Shoot(float playerBaseDamage)
    {
        if (gunInfo.currentAmmo > 0)
        {
            //Debug.Log(gunInfo.isAiming + ", isReloading:" + gunInfo.isReloading + " - " + CanShoot());

            if (CanShoot())
            {
                if (gunInfo.isMagReloaded)
                {
                    OnMagGunShoot(playerBaseDamage);
                    playerRigging.OnGunShoot();
                }
                else
                {
                    OnShotgunShoot(playerBaseDamage);
                    playerRigging.OnGunShoot();
                }

            }
        }
    }

    private void OnMagGunShoot(float playerBaseDamage)
    {

        Vector3 muzzleDirection = muzzle.transform.TransformDirection(Vector3.forward);
        //Transform bullet = Instantiate(bulletProjectile, muzzle.position,Quaternion.LookRotation(muzzleDirection)); //Usando o m�todo tradicional de instanciar uma bala nova
        //Debug.DrawRay(bullet.position, muzzleDirection*10,Color.blue);
        //Usando um game object pool, com as balas
        for (int i = 0; i <gunInfo.bulletsPerShot; i++)
        {
            GameObject bullet = bulletPool.GetPooledObject();

            bullet.GetComponent<Bullet>().SetDamage(gunInfo.damage * playerBaseDamage); //Seta o dano dessa balita
            bullet.transform.position = muzzle.position; //Bota a bala na posi��o certa
            
            bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * 50f; //Fazer algum calculo doido para velocidade da bala
            bullet.SetActive(true);
        }    
        //Sim, vai gastar só 1 munição mesmo tendo um moi de bala
            gunInfo.currentAmmo -= 1;
            timeSinceLastShot = 0f;

    }

    private void OnShotgunShoot(float playerBaseDamage)
    {
        Vector3 muzzleDirection = muzzle.transform.TransformDirection(Vector3.forward);
        for (int i = 0; i <gunInfo.bulletsPerShot; i++)
        {
            // Instantiate a bullet from the object pool
            GameObject bullet = bulletPool.GetPooledObject();
            Debug.Log("entrada: " + bullet.activeSelf);
            bullet.GetComponent<Bullet>().SetDamage(gunInfo.damage * playerBaseDamage); // Set the damage of the bullet
            bullet.transform.position = muzzle.position; // Set the position of the bullet to the muzzle position

            // Calculate a random direction and velocity for the bullet
            Vector3 randomDirection = Quaternion.Euler(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f) * muzzleDirection;
            float randomVelocity = Random.Range(40f, 50f);

            // Set the velocity of the bullet based on the random direction and velocity
            bullet.GetComponent<Rigidbody>().velocity = randomDirection * randomVelocity;

            // Activate the bullet
            bullet.SetActive(true);
        }

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
