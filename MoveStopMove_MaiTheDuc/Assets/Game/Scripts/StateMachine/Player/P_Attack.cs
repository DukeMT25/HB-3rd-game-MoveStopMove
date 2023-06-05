using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Attack : State
{
    private Player player;

    public P_Attack(Character character, Animator anim, int animName, Player player) : base(character, anim, animName)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();

        player.IsAtk = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.IsAtk = false;
    }

    public override void Tick()
    {
        base.Tick();
    }
}
