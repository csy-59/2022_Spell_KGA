using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForTitle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rigid;
    private PlayerInput input;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveOffset = input.X * moveSpeed * Time.fixedDeltaTime * transform.right
            + input.Z * moveSpeed * Time.fixedDeltaTime * transform.forward;

        rigid.MovePosition(rigid.position + moveOffset);
    }
}