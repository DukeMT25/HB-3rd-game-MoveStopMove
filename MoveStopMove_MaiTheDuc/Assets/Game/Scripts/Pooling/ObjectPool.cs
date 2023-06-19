using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    // initial number of cloned objects
    [SerializeField] private uint initPoolSize;

    // PooledObject prefab
    [SerializeField] private PooledObj objectToPool;
    // store the pooled objects in stack
    private Stack<PooledObj> stack;
    // Pool Masters objects
    [SerializeField] private GameObject PoolMasters;

    public PooledObj ObjectToPool { get => objectToPool; set => objectToPool = value; }
    public uint InitPoolSize => initPoolSize;

    private void Awake()
    {
        SetupPool();
    }
    //private void Start()
    //{
    //    SetupPool();
    //}

    // creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        // missing objectToPool Prefab field
        if (objectToPool == null)
        {
            return;
        }

        stack = new Stack<PooledObj>();

        // populate the pool
        PooledObj instance = null;

        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.Pool = this;
            //Set Pool Masters 
            instance.gameObject.transform.SetParent(PoolMasters.gameObject.transform);
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    // returns the first active GameObject from the pool
    public PooledObj GetPooledObject()
    {
        // missing objectToPool field
        if (objectToPool == null)
        {
            return null;
        }

        // if the pool is not large enough, instantiate extra PooledObjects
        if (stack.Count == 0)
        {
            PooledObj newInstance = Instantiate(objectToPool);
            newInstance.Pool = this;
            return newInstance;
        }

        // otherwise, just grab the next one from the list
        PooledObj nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public void ReturnToPool(PooledObj pooledObject)
    {
        pooledObject.gameObject.transform.SetParent(PoolMasters.gameObject.transform);
        pooledObject.gameObject.transform.position = Vector3.zero;
        pooledObject.gameObject.SetActive(false);
        stack.Push(pooledObject);
    }
}
