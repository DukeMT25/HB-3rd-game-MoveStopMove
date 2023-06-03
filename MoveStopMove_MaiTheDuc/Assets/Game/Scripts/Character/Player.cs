using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] FloatingJoystick _floatingJoystick;
    [SerializeField] Transform Ray;

    public Vector3 MoveDirection { get; set; }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInit()
    {
        base.OnInit();

        //StateMachine.Initialize();
    }

    protected override void Update()
    {
        base.Update();
        Moving();
        PullDown();
    }

    private void PullDown()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;
        //Debug.DrawRay(transform.position, dwn * 10f);
        if (Physics.Raycast(Ray.transform.position, dwn, out hit, 5f))
        {
            //if (!hit.transform.GetComponent<EndBridge>())
            //{
            //    transform.position = hit.point;
            //}
            transform.position = hit.point;
        }
    }

    private void Moving()
    {
        MoveDirection = new Vector3(_floatingJoystick.Horizontal, _rb.velocity.y, _floatingJoystick.Vertical) * base.moveSpeed;
        transform.position += MoveDirection;
        if (MoveDirection != Vector3.zero)
        {
            RotateTowards(gameObject, MoveDirection);
        }
    }

    private void RotateTowards(GameObject gameObject, Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
