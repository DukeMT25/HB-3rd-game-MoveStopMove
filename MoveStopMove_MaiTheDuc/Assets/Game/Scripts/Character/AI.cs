using System;
using UnityEngine;
using UnityEngine.AI;

public class AI : Character
{
    [Space, Header("AI Info")]
    [SerializeField] float idleTime = 1.5f;
    public float IdleTime => idleTime;
    [SerializeField] float patrolRadius = 7f;
    public float PatrolRadius => patrolRadius;

    public NavMeshAgent agent;


    #region States

    public AI_Idle IdleState { get; private set; }
    public AI_Run RunState { get; private set; }
    public AI_Atk AttackState { get; private set; }
    public AI_Dead DeadState { get; private set; }

    #endregion

    public static event EventHandler<OnAnyAIDeadArgs> onAnyAIDead;
    public class OnAnyAIDeadArgs : EventArgs
    {
        public AI _ai;
        public Character damageDealer;
    }

    public override void Start()
    {
        base.Start();

        onAnyAIDead += AI_onAnyAIDead;
    }

    private void AI_onAnyAIDead(object sender, AI.OnAnyAIDeadArgs e)
    {
        if (targetController.listEnemy.Contains(e._ai))
        {
            targetController.listEnemy.Remove(e._ai);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        Indicator.SetName(Constraint.GetRandomName());

        hp = 1;
        Exp = UnityEngine.Random.Range(1, 13);
        RandomLevel(Exp);

        weaponIndex = UnityEngine.Random.Range(0, ListWeaponsInHand.Count);

        ShowWeaponInHand();

        agent = GetComponent<NavMeshAgent>();

#pragma warning disable
        IdleState = new AI_Idle(this, _anim, Constraint.idleName);
        RunState = new AI_Run(this, _anim, Constraint.runName);
        AttackState = new AI_Atk(this, _anim, Constraint.atkName);
        DeadState = new AI_Dead(this, _anim, Constraint.deadName);
#pragma warning restore

        StateMachine.Initialize(IdleState);
    }

    private void RandomLevel(int lv)
    {
        transform.localScale = Vector3.one;
        if (Exp >= 2)
        {
            transform.localScale *= 1.2f;
        }
        if (Exp >= 7)
        {
            transform.localScale *= 1.4f;
        }
        if (Exp >= 12)
        {
            transform.localScale *= 1.1f;
        }

        Indicator.SetScore(lv);
    }

    protected override void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            base.Update();
        }
    }

    public override void OnDespawn()
    {
        ResetNavMesh();

        onAnyAIDead?.Invoke(this, new OnAnyAIDeadArgs { _ai = this });
        StateMachine.ChangeState(DeadState);

        Indicator.OnDespawn();
        levelManager.Bots.Remove(this);
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
}
