using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Character character;
    public List<Character> listEnemy = new List<Character>();

    private SphereCollider SphereCollider;

    //public Character Character { get => character; set => character = value; }

    private void Start()
    {
        SphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;

        if (listEnemy.Count > 0 && !Constraint.IsDes(character.transform.position, listEnemy[0].transform.position, SphereCollider.radius + 5))
        {
            listEnemy.Remove(listEnemy[0]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character enemy = other.GetComponent<Character>();

        if (enemy != null && enemy != character && !listEnemy.Contains(enemy) && !enemy.IsDead) { 
            listEnemy.Add(enemy);

        }
    }

    void OnTriggerStay(Collider other)
    {
        Character enemy = other.GetComponent<Character>();
        if (enemy != null && enemy != character && !listEnemy.Contains(enemy) && !enemy.IsDead)
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
        if (listEnemy.Count < 1) return null;

        else
        {
            CheckTarget();
            if (listEnemy != null) return listEnemy[0].gameObject;
            else return null;
        }
    }
    public void CheckTarget()
    {
        if (listEnemy[0].IsDead || !Constraint.IsDes(character.transform.position, listEnemy[0].transform.position, SphereCollider.radius + 5))
        {
            listEnemy.Remove(listEnemy[0]);
        }
    }
}
