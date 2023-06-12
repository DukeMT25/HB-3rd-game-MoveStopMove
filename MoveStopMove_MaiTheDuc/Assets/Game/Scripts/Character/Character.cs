using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Animator _anim;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected SkinnedMeshRenderer _chaMesh;
    [SerializeField] protected SkinnedMeshRenderer _pant;
    [SerializeField] Transform weaponTrans;
    [SerializeField] Transform weaponBase;
    [SerializeField] public Target targetController;
    [SerializeField] GameObject Axe2;


    public List<Material> _listPantsMat;
    public float attackTime = 3f;

    public StateMachine StateMachine { get; set; }
    protected bool isDead;
    public bool IsDead { get => isDead; set => isDead = value; }

    protected Vector3 startPosition;

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
            GameObject weaponObject = Instantiate(Axe2);
            weaponObject.transform.position = weaponBase.transform.position;
            weaponObject.transform.rotation = weaponBase.transform.rotation;
            //weaponObject.transform.Rotate(90, 0, 90);

            weaponObject.GetComponent<Weapon>().Shoot(targetController.TargetLock().transform);
        }
        weaponTrans.gameObject.SetActive(false);
    }

    public virtual void EndAttack()
    {
        weaponTrans.gameObject.SetActive(true);
    }

    protected virtual void OnDead(Character damageDealer)
    {
        isDead = true;
    }
}
