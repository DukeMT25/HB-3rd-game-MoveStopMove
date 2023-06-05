using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Ulti : State
{
    private Player player;

    public P_Ulti(Character character, Animator anim, int animName, Player player) : base(character, anim, animName)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();

        player.IsUlti = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.IsUlti = false;
    }

    public override void Tick()
    {
        base.Tick();
    }
}
