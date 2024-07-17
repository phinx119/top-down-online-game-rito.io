using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSpawn : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 30;
    private ObjectPool<MonoBehaviour> objectPool;

    void Start()
    {
        objectPool = new ObjectPool<MonoBehaviour>(prefab.GetComponent<MonoBehaviour>(), poolSize);
        SpawnAllObjects();
    }

    void Update()
    {
        EnsurePoolSize();
    }

    private void SpawnAllObjects()
    {
        foreach (MonoBehaviour obj in objectPool.GetAllPooledObjects())
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.transform.position = GetRandomPosition();
                obj.gameObject.SetActive(true);
            }
        }
    }

    private void EnsurePoolSize()
    {
        int activeCount = 0;
        foreach (MonoBehaviour obj in objectPool.GetAllPooledObjects())
        {
            if (obj.gameObject.activeInHierarchy)
            {
                activeCount++;
            }
        }

        while (activeCount < poolSize)
        {
            MonoBehaviour obj = objectPool.GetPooledObject();
            if (obj != null)
            {
                obj.transform.position = GetRandomPosition();
                obj.gameObject.SetActive(true);
                activeCount++;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        // Define the bounds for spawning
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        float z = 0;
        return new Vector3(x, y, z);
    }
}
