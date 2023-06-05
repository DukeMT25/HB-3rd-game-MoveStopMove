using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator _anim;
    [SerializeField] protected float moveSpeed;

    public StateMachine StateMachine { get; set; }

    protected Vector3 startPosition;

    protected virtual void Start()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        StateMachine = new StateMachine();

        startPosition = transform.position;
    }

    protected virtual void Update()
    {
        StateMachine.CurrentState.Tick();
    }


}
