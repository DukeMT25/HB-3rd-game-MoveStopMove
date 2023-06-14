using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Player : Character
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] FloatingJoystick _floatingJoystick;
    [SerializeField] Transform Ray;

    public Vector3 MoveDirection { get; set; }

    public P_Idle IdleState { get; set; }
    public P_Run RunState { get; set; }

    //Atk
    public P_Attack AtkState { get; set; }

    //Ulti
    public P_Ulti UltiState { get; set; }

    //Dead
    public P_Dead DeadState { get; set; }


    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInit()
    {
        base.OnInit();

        IdleState = new P_Idle(this, _anim, Constraint.idleName, this);
        RunState = new P_Run(this, _anim, Constraint.runName, this);
        AtkState = new P_Attack(this, _anim, Constraint.atkName, this);
        DeadState = new P_Dead(this, _anim, Constraint.deadName);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        Moving();
    }

    private void Moving()
    {
        MoveDirection = (Vector3.right * _floatingJoystick.Horizontal + Vector3.forward * _floatingJoystick.Vertical) * moveSpeed * Time.deltaTime;
        transform.position += MoveDirection;
        if (MoveDirection != Vector3.zero)
        {
            RotateTowards(gameObject, MoveDirection);
        }
    }

    private void RotateTowards(GameObject gameObject, Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void EndAttack()
    {
        base.EndAttack();
    }

    //protected override void OnDead(Character damageDealer)
    //{
    //    base.OnDead(damageDealer);

    //    StateMachine.ChangeState(DeadState);
    //    MoveDirection = Vector3.zero;

    //    //UIManager.Instance.SwitchToRevivePanel();
    //    //GameManager.Instance.GameOver();
    //}
    protected override void OnDead()
    {
        base.OnDead();

        StateMachine.ChangeState(DeadState);
        MoveDirection = Vector3.zero;

        //UIManager.Instance.SwitchToRevivePanel();
        //GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null && weapon._character != this)
        {
            OnDead();
        }
    }
}
