using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : GameUnit
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Character _character;

    private LevelManager _levelManager;

    public bool Select;
    public bool Equipped;
    public bool Buyed;

    private Vector3 target;
    private Vector3 startPoint;
    private Vector3 direction;

    public WeaponType WeaponType { get => _weaponType; set => _weaponType = value; }
    public Character Character { get => _character; set => _character = value; }
    public Vector3 Target { get => target; set => target = value; }
    public Vector3 StartPoint { get => startPoint; set => startPoint = value; }
    public Vector3 Direction { get => direction; set => direction = value; }

    private void Start()
    {
        _levelManager = LevelManager.Instance;
    }

    public void Throw()
    {
        if (_weaponType == WeaponType.Arrow)
        {
            //Xoay Weapon to Enemy
            if (direction.x <= 0)
            {
                SetRotation(Vector3.forward);
            }
            else
            {
                SetRotation(Vector3.back);
            }
        }
        else
        {
            //rotY += Time.deltaTime * rotationSpeed;
            //transform.rotation = Quaternion.Euler(90, rotY, 0);
            LeanTween.rotateY(gameObject, 306f * 3, 0.5f);
        }

        //Vector3 TargetPoint = new Vector3(transform.position.x + direction.x * bulletSpeed * Time.deltaTime, transform.position.y, transform.position.z + direction.z * bulletSpeed * Time.deltaTime);
        //transform.position = TargetPoint;

        Vector3 targetPos = new Vector3(Target.x, transform.position.y, Target.z);

        LeanTween.move(gameObject, targetPos, 0.5f).setOnComplete(() =>
        {
            RemoveWeapon();
        });
    }

    private void SetRotation(Vector3 upwards)
    {
        Vector3 relativePos = Target - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, upwards);

        transform.rotation = rotation;
        transform.eulerAngles += new Vector3(0, 90, 0);
    }

    private void RemoveWeapon()
    {
        _levelManager.Weapons.Remove(this);
        OnDespawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        Character _target = other.GetComponent<Character>();
        if (_target && _target != Character)
        {
            RemoveWeapon();
            _target.OnHit(1f);

            _character.Exp += 1;
            Player _player = _character.GetComponent<Player>();
            if (_player != null) _player.Coin += 1;

            _character.LevelUp();
        }
    }

    public override void OnInit() { }

    public override void OnDespawn() 
    {
        SimplePool.Despawn(this);
    }

}
