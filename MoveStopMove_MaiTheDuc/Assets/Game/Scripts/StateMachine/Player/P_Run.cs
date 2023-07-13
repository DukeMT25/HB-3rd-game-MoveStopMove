using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Run : P_State
{
    public P_Run(Character character, Animator anim, int animName, Player player) : base(character, anim, animName)
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
        player.attackTime = 0.3f;
        if (player.MoveDirection == Vector3.zero)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
    }
}
