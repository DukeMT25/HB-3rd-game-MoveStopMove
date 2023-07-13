using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField] public Animator _anim;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected SkinnedMeshRenderer _chaMesh;
    [SerializeField] protected SkinnedMeshRenderer _pant;
    [SerializeField] private List<GameObject> listWeaponsInHand;
    [SerializeField] public Target targetController;
    [SerializeField] private Transform weaponStartPoint;
    [SerializeField] Transform _indicatorPoint;

    [SerializeField] private int exp;

    protected Rigidbody rb;
    private Indicator indicator;

    protected GameManager gameManager;
    protected LevelManager levelManager;

    public List<Material> _listPantsMat;
    public float attackTime = 1f;

    public StateMachine StateMachine { get; set; }

    public float hp = 1f;
    public bool IsDead => hp <= 0;

    public Transform WeaponStartPoint { get => weaponStartPoint; set => weaponStartPoint = value; }
    public List<GameObject> ListWeaponsInHand { get => listWeaponsInHand; set => listWeaponsInHand = value; }
    public int Exp { get => exp; set => exp = value; }
    public Indicator Indicator { get => indicator; set => indicator = value; }

    public int weaponIndex;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager = GameManager.Instance;
        levelManager = LevelManager.Instance;

        Exp = 0;
        SetPant(UnityEngine.Random.Range(0, 9));

        OnInit();
    }

    public override void OnInit()
    {
        if (StateMachine == null)
        {
            StateMachine = new StateMachine();
        }

        Indicator = SimplePool.Spawn<Indicator>(PoolType.Indicator);
        Indicator.SetTarget(_indicatorPoint);
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
        if (transform != null) StateMachine.CurrentState.Tick();
    }

    public virtual void LevelUp()
    {
        if (Exp == 2)
        {
            transform.localScale *= 1.2f;
        }
        else if (Exp == 7)
        {
            transform.localScale *= 1.4f;
        }
        else if (Exp == 12)
        {
            transform.localScale *= 1.1f;
        }
    }

    public void SetPant(int pantId)
    {
        if (pantId < _listPantsMat.Count)
        {
            _chaMesh.material = _listPantsMat[pantId];
            _pant.material = _listPantsMat[pantId];
        }
    }

    public void Attack()
    {
        if (targetController.TargetLock() != null && targetController.listEnemy.Count > 0)
        {
            Vector3 WeaponDirection = new Vector3(targetController.TargetLock().transform.position.x - weaponStartPoint.transform.position.x, 
                                                    rb.velocity.y, 
                                                    targetController.TargetLock().transform.position.z - weaponStartPoint.transform.position.z).normalized;
            ThrowWeapon(WeaponDirection);
        }
    }

    public void ThrowWeapon(Vector3 direction)
    {
        Weapon weaponObject = ListWeaponsInHand[weaponIndex].GetComponent<Weapon>();

        Weapon weapon = SimplePool.Spawn<Weapon>((PoolType)(weaponObject.WeaponType + 1));
        
        weapon.transform.position = weaponStartPoint.transform.position;

        //Target
        Vector3 newTarget = new Vector3(targetController.TargetLock().transform.position.x, weapon.transform.position.y, targetController.TargetLock().transform.position.z);
        weapon.Target = newTarget;

        //Component
        weapon.Direction = direction;
        weapon.StartPoint = transform.position;
        weapon.Character = this;

        levelManager.Weapons.Add(weapon);

        weapon.Throw();
    }

    public void OnHit(float damage)
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

    public override void OnDespawn() { }
}
