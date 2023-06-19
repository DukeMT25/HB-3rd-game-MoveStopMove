using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObj : MonoBehaviour
{
    private ObjectPool pool;
    public ObjectPool Pool { get => pool; set => pool = value; }

    public void Release()
    {
        pool.ReturnToPool(this);
    }
    public void OnDespawn()
    {
        Destroy(pool);
    }

    public PooledObj Spawner(ObjectPool a_obj, GameObject a_root, bool enable)
    {
        PooledObj _pooledObject = a_obj.GetPooledObject();
        _pooledObject.transform.SetParent(a_root.transform);
        _pooledObject.gameObject.SetActive(enable);
        return _pooledObject;
    }
    public PooledObj Spawner(ObjectPool a_obj, GameObject a_root)
    {
        PooledObj _pooledObject = a_obj.GetPooledObject();
        _pooledObject.transform.SetParent(a_root.transform);
        return _pooledObject;
    }
    public PooledObj Spawner(ObjectPool a_obj, bool enable)
    {
        PooledObj _pooledObject = a_obj.GetPooledObject();
        return _pooledObject;
    }
}
