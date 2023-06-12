using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Idle : AI_State
{
    private float idleTime;

    public AI_Idle(Character character, Animator anim, int animString) : base(character, anim, animString)
    {
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = _ai.IdleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        idleTime -= Time.deltaTime;

        if (_ai.targetController.listEnemy.Count > 0)
        {
            _ai.attackTime -= Time.deltaTime;
            if (_ai.attackTime <= 0)
            {
                _ai.StateMachine.ChangeState(_ai.AttackState);
            }
        }
        else if (idleTime < 0f && _ai.targetController.listEnemy.Count == 0)
        {
            _ai.StateMachine.ChangeState(_ai.RunState);
        }


    }
}
