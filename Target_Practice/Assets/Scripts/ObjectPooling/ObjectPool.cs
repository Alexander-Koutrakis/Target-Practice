using System.Collections.Generic;
using UnityEngine;

//Generic object pooling system for monobehaviours
public class ObjectPool <T>where T:IPoolObject
{
    protected List<T> pool = new List<T>();
    protected GameObject poolObjectPrefab;

    //Add a new gameobject to the pool
    private void AddObject()
    {
        GameObject newPoolGameObject = GameObject.Instantiate(poolObjectPrefab);
        newPoolGameObject.SetActive(false);
        T newPoolObject = newPoolGameObject.GetComponent<T>();
        newPoolObject.Initialize();
        pool.Add(newPoolObject);
    }

    //Add a new object to the pool and return it
    private T GetNewObject()
    {
        GameObject newPoolGameObject = GameObject.Instantiate(poolObjectPrefab);
        newPoolGameObject.SetActive(false);
        T newPoolObject = newPoolGameObject.GetComponent<T>();
        newPoolObject.Initialize();
        pool.Add(newPoolObject);
        return newPoolObject;
    }

    //get object from the pool
    public T GetObject()
    {
       foreach(T gameObject in pool)
       {
            if (!gameObject.IsActive())
            {
                return gameObject;
            }
       }

            return GetNewObject();
    }
    public ObjectPool(GameObject poolObject)
    {
        this.poolObjectPrefab = poolObject;
        this.poolObjectPrefab.SetActive(false);
        AddObject();
    }

}
