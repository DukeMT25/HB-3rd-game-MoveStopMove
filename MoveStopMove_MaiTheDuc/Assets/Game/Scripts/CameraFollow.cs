using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public float speed = 20;

    Vector3 offset;

    public Camera Camera;

    void Start()
    {
        Camera = Camera.main;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
        }
    }

    public void Higher(float y)
    {
        offset += Vector3.up * y;
        offset += Vector3.back * y / 2;
    }

    public void Lower(float x)
    {
        transform.rotation = Quaternion.LookRotation(-offset);
    }

    public void ResetOffset()
    {
        offset = Vector3.zero;
        offset = transform.position - target.position;
    }
}
