using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_State : State
{
    protected AI _ai;

    public AI_State(Character character, Animator anim, int animName) : base(character, anim, animName)
    {
        _ai = character as AI;
    }
}
