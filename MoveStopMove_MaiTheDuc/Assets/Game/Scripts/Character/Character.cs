using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected SkinnedMeshRenderer _chaMesh;
    [SerializeField] protected SkinnedMeshRenderer _pant;
    [SerializeField] Transform weaponTrans;
    [SerializeField] Transform weaponBase;
    [SerializeField] protected Target targetController;
    [SerializeField] GameObject Axe2;


    public List<Material> _listPantsMat;
    public float attackTime = 3f;

    public StateMachine StateMachine { get; set; }

    protected Vector3 startPosition;

    protected virtual void Start()
    {
        OnInit();

        SetPant(Random.Range(0,9));
    }

    protected virtual void OnInit()
    {
     
        StateMachine = new StateMachine();

        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Tick();
        attackTime -= Time.deltaTime;
        if (attackTime <= 0)
        {
            Attack();
        }
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
        if(targetController != null && targetController.listEnemy.Count > 0)
        {
            attackTime = 3f;

        }
    }

    public void ThrowWeapon()
    {
        if (targetController.TargetLock() != null)
        {
            GameObject weaponObject = GameObject.Instantiate(Axe2);
            weaponObject.transform.position = weaponBase.transform.position;
            weaponObject.transform.rotation = weaponBase.transform.rotation;

            weaponObject.GetComponent<Weapon>().Shoot(targetController.TargetLock().transform);
        }
        weaponTrans.gameObject.SetActive(false);
    }

    public virtual void EndAttack()
    {
        weaponTrans.gameObject.SetActive(true);
    }
}
