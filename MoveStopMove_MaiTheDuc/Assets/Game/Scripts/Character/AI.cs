using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

public class AI : Character
{
    [Space, Header("AI Info")]
    [SerializeField] float idleTime = 3f;
    public float IdleTime => idleTime;
    [SerializeField] float patrolRadius = 7f;
    public float PatrolRadius => patrolRadius;

    public UnityEngine.AI.NavMeshAgent agent;

    public bool IsPause { get; set; }


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

        weaponIndex = UnityEngine.Random.Range(0, ListWeaponsInHand.Count);

        ShowWeaponInHand();

            //ObjectPool objpool = gameManager.WeaponObjectPool[weaponIndex];

            //for (int i = 0; i < 2; i++)
            //{
            //    Weapon weapon = gameManager.Weaponspawner.SpawnWeapon(gameManager.WeaponHolder, objpool);
            //    _listWeaponatk.Add(weapon);
            //}

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
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
    }

    BotPool BotPool;
    public override void OnDespawn()
    {
        base.OnDespawn();

        ResetNavMesh();
        StateMachine.ChangeState(DeadState);
        BotPool.RemoveBot(this);
        onAnyAIDead?.Invoke(this, new OnAnyAIDeadArgs { _ai = this });
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
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
