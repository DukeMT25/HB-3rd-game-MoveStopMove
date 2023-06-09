using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class P_Attack : P_State
{
    bool atked = false;

    public P_Attack(Character character, Animator anim, int animName, Player player) : base(character, anim, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Character target;

        if (player.targetController.listEnemy.Count > 0)
        {
            target = player.targetController.listEnemy[0];

            player.transform.LookAt(target.transform.position); 
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.ShowWeaponInHand();
        atked = false;
    }

    public override void Tick()
    {
        base.Tick();
        if (player.MoveDirection != Vector3.zero)
        {
            player.StateMachine.ChangeState(player.RunState);
        }

        if (player._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4 && atked == false)
        {
            player.Attack();
            atked = true;
        }

        else if (player._anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            player.attackTime = 1f;
            player.StateMachine.ChangeState(player.IdleState);
            atked = false;
            player.ShowWeaponInHand();
        }
    }
}
