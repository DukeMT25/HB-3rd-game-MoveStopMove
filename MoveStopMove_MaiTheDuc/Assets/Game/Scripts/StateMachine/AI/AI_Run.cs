using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Run : AI_State
{
    public AI_Run(Character character, Animator anim, int animName) : base(character, anim, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Vector3 targetPos = new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
        _ai.SetDestination(targetPos);
        //Debug.Log(targetPos);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
        _ai.attackTime = 0.5f;
        if (_ai.IsAtDestination())
        {
            _ai.StateMachine.ChangeState(_ai.IdleState);
        }

        if (_ai.targetController.listEnemy.Count > 0)
        {
            _ai.agent.isStopped = true;
            _ai.StateMachine.ChangeState(_ai.IdleState);
        }
        else _ai.agent.isStopped = false;
    }
}
