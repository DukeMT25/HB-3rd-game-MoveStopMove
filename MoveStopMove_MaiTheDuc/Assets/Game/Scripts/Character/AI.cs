using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : Character
{
    public static event EventHandler<OnAnyAIDeadArgs> onAnyAIDead;
    public class OnAnyAIDeadArgs : EventArgs
    {
        public AI _ai;
        public Character damageDealer;
    }


    [Space, Header("AI Info")]
    [SerializeField] float idleTime = 3f;
    public float IdleTime => idleTime;
    [SerializeField] float patrolRadius = 7f;
    public float PatrolRadius => patrolRadius;
    [SerializeField] GameObject targetVisual;

    public UnityEngine.AI.NavMeshAgent agent;

    public bool IsPause { get; set; }


    #region States

    public AI_Idle IdleState { get; private set; }
    public AI_Run RunState { get; private set; }
    public AI_Atk AttackState { get; private set; }
    public AI_Dead DeadState { get; private set; }

    #endregion

    protected override void OnInit()
    {
        base.OnInit();

        //IsPause = false;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //IndicatorManager.Instance.CreateNewIndicator(this);

        #pragma warning disable
        IdleState = new AI_Idle(this, _anim, Constraint.idleName);
        RunState = new AI_Run(this, _anim, Constraint.runName);
        AttackState = new AI_Atk(this, _anim, Constraint.atkName);
        DeadState = new AI_Dead(this, _anim, Constraint.deadName);
        #pragma warning restore

        StateMachine.Initialize(IdleState);
        SetEnemyAsTarget(false);
    }

    //public override void OnNewGame()
    //{
    //    base.OnNewGame();

    //    StateMachine.Initialize(IdleState);

    //}

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        //if (IsPause) return;

        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void EndAttack()
    {
        base.EndAttack();
    }

    protected override void OnDead()
    {
        base.OnDead();

        ResetNavMesh();
        SetEnemyAsTarget(false);
        StateMachine.ChangeState(DeadState);
        //onAnyAIDead?.Invoke(this, new OnAnyAIDeadArgs { _ai = this, damageDealer = damageDealer });
        onAnyAIDead?.Invoke(this, new OnAnyAIDeadArgs { _ai = this });
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null && weapon._character != this)
        {
            OnDead();
        }
    }

    public void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
    }

    public void ResetNavMesh()
    {
        agent.ResetPath();
    }

    public bool IsAtDestination() => !agent.pathPending && !agent.hasPath;

    public void SetEnemyAsTarget(bool isTarget)
    {
        if (StateMachine.CurrentState == DeadState) return;
        targetVisual.SetActive(isTarget);
    }

    //public override void ReleaseSelf()
    //{
    //    base.ReleaseSelf();

    //    //IndicatorManager.Instance.RemoveIndicator(this);
    //}
}
