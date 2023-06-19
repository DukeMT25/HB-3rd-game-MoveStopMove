using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : PooledObj
{
    public Weapon SpawnWeapon (GameObject weaponHolder, ObjectPool pooled)
    {
        PooledObj weaponPool = Spawner(pooled, weaponHolder, false);
        Weapon weapon = weaponPool.GetComponent<Weapon>();
        return weapon;
    }


}
