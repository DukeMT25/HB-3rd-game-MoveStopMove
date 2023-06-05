using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    private bool isAtk;
    public bool IsAtk { get => isAtk; set => isAtk = value; }

    //Ulti
    public P_Ulti UltiState { get; set; }
    private bool isUlti;
    public bool IsUlti { get => isUlti; set => isUlti = value; }


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

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        Moving();
        PullDown();
    }

    private void PullDown()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        //Debug.DrawRay(transform.position, dwn * 10f);
        if (Physics.Raycast(Ray.transform.position, dwn, out hit, 5f))
        {
            transform.position = hit.point;
        }
    }

    private void Moving()
    {
        //MoveDirection = new Vector3(_floatingJoystick.Horizontal, _rb.velocity.y, _floatingJoystick.Vertical) * moveSpeed * Time.deltaTime;
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
}
