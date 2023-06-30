using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] public Animator _anim;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected SkinnedMeshRenderer _chaMesh;
    [SerializeField] protected SkinnedMeshRenderer _pant;
    [SerializeField] List<GameObject> listWeaponsInHand;
    [SerializeField] public Target targetController;

    [SerializeField] Transform weaponStartPoint; // khi sinh vu lay toa  poss world

    public GameManager gameManager;

    public List<Weapon> _listWeaponatk;
    public ObjectPool PoolObject { get; set; }

    public List<Material> _listPantsMat;
    public float attackTime = 0.5f;

    public StateMachine StateMachine { get; set; }

    public float hp = 1f;
    public bool IsDead => hp <= 0;

    //STAT
    private float inGameAttackRange = 7.0f;

    public Transform WeaponStartPoint { get => weaponStartPoint; set => weaponStartPoint = value; }
    public List<GameObject> ListWeaponsInHand { get => listWeaponsInHand; set => listWeaponsInHand = value; }
    public float InGameAttackRange { get => inGameAttackRange; set => inGameAttackRange = value; }

    public int weaponIndex;

    public virtual void Start()
    {
        gameManager = GameManager.Instance;

        SetPant(UnityEngine.Random.Range(0, 9));
        OnInit();
    }

    public override void OnInit()
    {
        if (StateMachine == null)
        {
            StateMachine = new StateMachine();
        }

        _listWeaponatk = new List<Weapon>();
    }

    public void HideAllWeapon()
    {
        for (int i = 0; i < listWeaponsInHand.Count; i++)
        {
            ListWeaponsInHand[i].SetActive(false);
        }
    }

    public virtual void ShowWeaponInHand()
    {
        HideAllWeapon();
        ListWeaponsInHand[weaponIndex].SetActive(true);
    }

    protected virtual void Update()
    {
        if (transform != null)
            StateMachine.CurrentState.Tick();
    }

    public void SetPant(int pantId)
    {
        if(pantId < _listPantsMat.Count)
        {
            _chaMesh.material = _listPantsMat[pantId];
            _pant.material = _listPantsMat[pantId];
        }
    }

    public virtual void Attack()
    {
        if (targetController != null && targetController.listEnemy.Count > 0)
        {
            ThrowWeapon();
        }
    }

    public void ThrowWeapon()
    {
        if (targetController.TargetLock() != null)
        {
            Weapon weaponObject = _listWeaponatk[0];
            weaponObject.gameObject.SetActive(true);
            weaponObject.transform.position = weaponStartPoint.transform.position;

            weaponObject.GetComponent<Weapon>().Shoot(targetController.TargetLock().transform, this);

            
        }
    }

    public virtual void OnHit(float damage)
    {

        //if (!IsDead)
        //{
        //    //if (InCamera(_GameManager.MainCam))
        //    //{
        //    //    _GameManager.SoundManager.PlayWeaponHitSoundEffect();
        //    //}

        hp -= damage;

        if (IsDead)
        {
            OnDespawn();
        }
    }

    public override void OnDespawn()
    {
        throw new NotImplementedException();
    }
}
