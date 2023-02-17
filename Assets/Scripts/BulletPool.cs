using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //Precisa de um game object pra fazer pool
    public GameObject bulletPrefab;
    //Criar uma list para esse object
    private List<GameObject> bulletsList;

    public int poolSize;

    // Start is called before the first frame update
    private void Start()
    {
        bulletsList = new List<GameObject>();
        for (int i = 0; i< poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bulletsList.Add(bullet);
            bullet.transform.parent = transform;
            bullet.SetActive(false);
        }
    }
    //Retorna uma bala valida (ou seja, uma bala que nao esta ativa, caso não tenha nenhuma ativa na lista, criara uma bala nova)
    public GameObject GetBullet()
    {
        //Se todas as balas da lista presente no GameObjectPool estiverem ativas(não podemos instanciar balas ativas), ele ira criar novas balas para a lista
        foreach (GameObject bullet in bulletsList)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(bulletPrefab);
        bulletsList.Add(newBullet);
        newBullet.SetActive(true);
        newBullet.transform.parent = transform;
        return newBullet;
    }
    
}
