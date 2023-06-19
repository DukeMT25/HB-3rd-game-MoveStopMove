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
    //public WeaponSpawner weaponSpawner;

    public List<Weapon> _listWeaponatk;
    public ObjectPool PoolObject { get; set; }

    public List<Material> _listPantsMat;
    public float attackTime = 3f;

    public StateMachine StateMachine { get; set; }

    public float hp = 1f;
    //protected bool isDead;
    public bool IsDead => hp <= 0;

    public Transform WeaponStartPoint { get => weaponStartPoint; set => weaponStartPoint = value; }
    public List<GameObject> ListWeaponsInHand { get => listWeaponsInHand; set => listWeaponsInHand = value; }

    protected Vector3 startPosition;

    int weaponIndex;

    public void Awake()
    {
        gameManager = GameManager.Instance;
    }

    protected virtual void Start()
    {

        OnInit();

        SetPant(Random.Range(0,9));
    }

    protected virtual void OnInit()
    {
        if (StateMachine == null)
        {
            StateMachine = new StateMachine();
        }

        startPosition = transform.position;

        _listWeaponatk = new List<Weapon>();

        //Tao Weapon in WeaponHolder...
        // lay Weapon Index; Get PLayerPref.....
        weaponIndex = Random.Range(0, listWeaponsInHand.Count);

        //int theIndex = Random.Range(0, listWeaponsInHand.Count);
        //Todo Show weapon In Hand ShowWeaponInHand(weaponIndex);
        //weaponSpawner.WeaponObjectPool = new List<ObjectPool>();

        Debug.Log(weaponIndex);

        ShowWeaponInHand();
        for (int i = 0; i < 2; i++)
        {
            Weapon weapon = gameManager.Weaponspawner.SpawnWeapon(gameManager.WeaponHolder, gameManager.WeaponObjectPool[weaponIndex]);

            _listWeaponatk.Add(weapon);
        }

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
        //if (StateMachine.CurrentState != null)
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
            //GameObject weaponObject = Instantiate(Axe2);
            Weapon weaponObject = _listWeaponatk[0];
            weaponObject.gameObject.SetActive(true);
            weaponObject.transform.position = weaponStartPoint.transform.position;
            weaponObject.transform.rotation = weaponStartPoint.transform.rotation;
            ////weaponObject.transform.Rotate(90, 0, 90);

            weaponObject.GetComponent<Weapon>().Shoot(targetController.TargetLock().transform, this);

            
        }
        //hide weapon index in ListweaponInHand
        //weaponTrans.gameObject.SetActive(false);
    }

    protected virtual void OnDead()
    {
        //IsDead = true;
    }

    public virtual void OnHit(float damage)
    {

        //if (!IsDead)
        //{
        //    //if (InCamera(_GameManager.MainCam))
        //    //{
        //    //    _GameManager.SoundManager.PlayWeaponHitSoundEffect();
        //    //}

        //    hp -= damage;
        //    if (IsDead)
        //    {
        //        hp = 0;
        //        OnDead();
        //    }
        //}
        hp -= damage;
        Debug.Log(hp);
        if (IsDead)
        {
            OnDead();
        }
    }
}
