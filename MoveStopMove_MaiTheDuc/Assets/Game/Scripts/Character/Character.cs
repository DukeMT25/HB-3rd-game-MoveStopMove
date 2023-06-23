using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Character : MonoBehaviour
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
    //protected bool isDead;
    public bool IsDead => hp <= 0;

    public Transform WeaponStartPoint { get => weaponStartPoint; set => weaponStartPoint = value; }
    public List<GameObject> ListWeaponsInHand { get => listWeaponsInHand; set => listWeaponsInHand = value; }

    protected Vector3 startPosition;

    public int weaponIndex;

    public virtual void Start()
    {
        gameManager = GameManager.Instance;

        SetPant(Random.Range(0, 9));
        OnInit();
    }

    public virtual void OnInit()
    {
        if (StateMachine == null)
        {
            StateMachine = new StateMachine();
        }

        startPosition = transform.position;

        _listWeaponatk = new List<Weapon>();
    }

    public void HideAllWeapon()
    {
        for (int i = 0; i < listWeaponsInHand.Count; i++)
        {
            ListWeaponsInHand[i].SetActive(false);
        }
    }

    public void ShowWeaponInHand()
    {
        HideAllWeapon();
        ListWeaponsInHand[weaponIndex].SetActive(true);
    }

    protected virtual void Update()
    {
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
        Debug.Log("attack");
        if (targetController != null && targetController.listEnemy.Count > 0)
        {
            ThrowWeapon();
        }
    }

    public void ThrowWeapon()
    {
        Debug.Log("throw");
        if (targetController.TargetLock() != null)
        {
            Debug.Log("throw success");

            Weapon weaponObject = _listWeaponatk[0];
            weaponObject.gameObject.SetActive(true);
            weaponObject.transform.position = weaponStartPoint.transform.position;

            weaponObject.GetComponent<Weapon>().Shoot(targetController.TargetLock().transform, this);

            
        }
    }

    protected virtual void OnDead()
    {
        //gameObject.GetComponent<PooledObj>().Release();
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
        Debug.Log(hp);
        if (IsDead)
        {
            OnDead();
        }
    }
}
