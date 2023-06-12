using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Dead : AI_State
{
    private float timer = 3f;

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

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            //_ai.ReleaseSelf();
        }
    }
}
