using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    Transform _target;

    public void Shoot(Transform target)
    {
        _target = target;
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        LeanTween.move(gameObject, targetPos, 0.5f).setOnComplete(() =>
        {
            GameObject.Destroy(gameObject);
        });
    }
}
