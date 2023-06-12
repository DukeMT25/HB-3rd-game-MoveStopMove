using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Idle : P_State
{
    public P_Idle(Character character, Animator anim, int animName, Player player) : base(character, anim, animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Tick()
    {
        base.Tick();
        if (player.MoveDirection != Vector3.zero)
        {
            player.StateMachine.ChangeState(player.RunState);
        }
        else if (player.targetController.listEnemy.Count > 0 && player.MoveDirection == Vector3.zero)
        {
            player.attackTime -= Time.deltaTime;
            if (player.attackTime <= 0)
            {
                player.StateMachine.ChangeState(player.AtkState);
            }
        }
        else return;
    }
}
