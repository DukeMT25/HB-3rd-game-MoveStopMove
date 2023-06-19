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
            ReleaseWeapon();
        });
    }

    private void ReleaseWeapon()
    {
        gameObject.GetComponent<PooledObj>().Release();
        _character.ShowWeaponInHand();
    }

    private void OnTriggerEnter(Collider other)
    {
        Character _target = other.GetComponent<Character>();
        if (_target && _target != _character && other.gameObject != gameObject)
        {
            _target.OnHit(1f);
            if (other.gameObject.GetComponent<Player>())
            {
                
            }
            else
            {

            }
        }
    }
}
