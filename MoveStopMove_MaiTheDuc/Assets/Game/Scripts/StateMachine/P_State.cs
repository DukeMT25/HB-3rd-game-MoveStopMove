using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_State : State
{
    protected Player player;

    public P_State(Character character, Animator anim, int animString) : base(character, anim, animString)
    {
        player = character as Player;
    }
}
