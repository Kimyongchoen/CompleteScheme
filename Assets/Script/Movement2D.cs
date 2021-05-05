using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed;

    public bool MoveFlag = false;

    private void Update()
    {
        if(MoveFlag)
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void MoveStart()
    {
        MoveFlag = true;
    }
    public void MoveStop()
    {
        MoveFlag = false;
    }


}
