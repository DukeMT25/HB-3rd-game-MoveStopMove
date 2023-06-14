using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    Transform _target;
    public Character _character;

    public void Shoot(Transform target, Character character)
    {
        _character = character;
        _target = target;
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        LeanTween.move(gameObject, targetPos, 0.5f).setOnComplete(() =>
        {
            GameObject.Destroy(gameObject);
        });
    }
}
