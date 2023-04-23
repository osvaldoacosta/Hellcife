using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Learned from https://learn.unity.com/tutorial/introduction-to-object-pooling
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake(){
        SharedInstance= this;
    }

    void Start(){
        pooledObjects= new List<GameObject>();
        GameObject tmp;
        for(int i= 0; i < amountToPool; i++)
        {
            tmp= Instantiate(objectToPool);
            tmp.transform.parent = transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject(){
        for(int i= 0; i < amountToPool; i++){
            if(!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        return null;
    }
    public bool hasAnyEnabled(){
        for(int i= 0; i < amountToPool; i++){
            if(pooledObjects[i].activeInHierarchy){
                return true;
            }
        }
        return false;
    }
}
