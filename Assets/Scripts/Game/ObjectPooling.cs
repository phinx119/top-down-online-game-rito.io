using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T prefab;
    public int poolSize;
    private List<T> pool;

    public ObjectPool(T prefab, int poolSize)
    {
        this.prefab = prefab;
        this.poolSize = poolSize;
        pool = new List<T>();

        for (int i = 0; i < poolSize; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public T GetPooledObject()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    public List<T> GetAllPooledObjects()
    {
        return pool;
    }
}
