using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMono<PoolManager>
{
    public Dictionary<string,PoolItem> dictPools = new Dictionary<string, PoolItem>();
    [SerializeField] private List<PoolItem> poolItems = new List<PoolItem>();

    private void Start()
    {
        foreach (var poolItem in poolItems)
        {
            poolItem.SetupPoolItem();
            dictPools.Add(poolItem.namePool, poolItem);
        }
    }

    public void AddNewPool(PoolItem newPoolItem)
    {
        if (!dictPools.ContainsKey(newPoolItem.namePool))
        {
            newPoolItem.SetupPoolItem();
            dictPools.Add(newPoolItem.namePool, newPoolItem);
        }
    }

    public Transform CreateNewPrefab(Transform prefab)
    {
        return Instantiate(prefab);
    }
}

[Serializable]
public class PoolItem
{
    public string namePool;
    private List<Transform> listGameObjects = new List<Transform>();
    public Transform prefab;
    public int maxObject;

    private int index = -1;

    public void SetupPoolItem()
    {
        for (int i = 0; i < maxObject; i++)
        {
            Transform trans = PoolManager.Instance.CreateNewPrefab(prefab);
            trans.gameObject.SetActive(false);
            listGameObjects.Add(trans);
            trans.hideFlags = HideFlags.HideInHierarchy;
        }
    }

    public Transform GetObjectInstance()
    {
        index++;
        if (index >= maxObject)
        {
            index = 0;
        }
        Transform trans = listGameObjects[index];
        trans.gameObject.SetActive(true);
        return trans;
    }

    public void DisableObjectPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
