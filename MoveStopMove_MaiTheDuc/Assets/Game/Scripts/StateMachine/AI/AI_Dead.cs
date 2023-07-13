using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dead : AI_State
{
    public AI_Dead(Character character, Animator anim, int animName) : base(character, anim, animName)
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

        if (_ai._anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SimplePool.Despawn(_ai);
            SimplePool.Despawn(_ai.Indicator);
            LevelManager.Instance.BotsInGame--;
        }
    }
}
