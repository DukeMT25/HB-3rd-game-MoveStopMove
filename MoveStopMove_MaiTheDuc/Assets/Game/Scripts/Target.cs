using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Character character;
    public List<Character> listEnemy = new List<Character>();

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if(enemy != character && enemy != null  && !listEnemy.Contains(enemy) && !enemy.IsDead)
        {
            listEnemy.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if(enemy != null && listEnemy.Contains(enemy))
        {
            listEnemy.Remove(enemy);
        }
    }

    public GameObject TargetLock()
    {
        if (listEnemy.Count == 0) return null;
        else return listEnemy[0].gameObject;
    }
}
