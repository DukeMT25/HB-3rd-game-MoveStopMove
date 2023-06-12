using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Atk : AI_State
{
    public AI_Atk(Character character, Animator anim, int animString) : base(character, anim, animString)
    {
    }

    public override void Enter()
    {
        base.Enter();


        Character target;

        if (_ai.targetController.listEnemy.Count > 0)
        {
            target = _ai.targetController.listEnemy[0];
            _ai.transform.LookAt(target.transform.position);
            _ai.Attack();
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();

        if (_ai._anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _ai.attackTime = 3.5f;
            _ai.StateMachine.ChangeState(_ai.IdleState);
        }

        else if (_ai.targetController.listEnemy.Count == 0)
        {
            _ai.StateMachine.ChangeState(_ai.RunState);
        }
    }
}
