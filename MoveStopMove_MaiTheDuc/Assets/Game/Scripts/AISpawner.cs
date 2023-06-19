using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : PooledObj
{
    public Character SpawnAI (GameObject aiHolder, ObjectPool pooled)
    {
        PooledObj aiPool = Spawner(pooled, aiHolder, false);
        Character character = aiPool.GetComponent<Character>();
        return character;
    }
}
