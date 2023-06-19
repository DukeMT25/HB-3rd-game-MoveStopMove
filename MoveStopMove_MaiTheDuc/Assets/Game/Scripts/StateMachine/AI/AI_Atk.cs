using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Atk : AI_State
{
    bool atked = false;

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
        }

    }

    public override void Exit()
    {
        base.Exit();
        _ai.ShowWeaponInHand();
        atked = true;
    }

    public override void Tick()
    {
        base.Tick();

        if (_ai._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3 && atked == false)
        {
                _ai.Attack();
    
            atked = true;
        }

        else if (_ai._anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            _ai.attackTime = 3.5f;
            _ai.StateMachine.ChangeState(_ai.IdleState);
            atked = false;
            _ai.ShowWeaponInHand();
        }

        else if (_ai.targetController.listEnemy.Count == 0)
        {
            _ai.StateMachine.ChangeState(_ai.RunState);
            atked = false;
        }
    }
}
