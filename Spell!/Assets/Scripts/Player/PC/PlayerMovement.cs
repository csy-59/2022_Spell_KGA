using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move For Pc")]
    [SerializeField] private float moveSpeedForPC = 5f;
    [SerializeField] private float sitSpeedForPc = 2f;

    public GameObject[] Colliders;

    protected Rigidbody rigid;
    protected PlayerInput input;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        SitAndSand(false);
    }

    protected virtual void FixedUpdate()
    {
        if(!UIManager.Instance.IsUIShown)
        {
            Move();
            SitAndSand(input.Shift);
        }
    }

    protected void Move(float speed, Transform targetTransform)
    {

        Vector3 moveOffset = input.X * speed * Time.fixedDeltaTime * targetTransform.right
            + input.Z * speed * Time.fixedDeltaTime * targetTransform.forward;

        rigid.MovePosition(rigid.position + moveOffset);
    }

    protected virtual void Move()
    {
        float speed = input.Shift ? sitSpeedForPc : moveSpeedForPC;
        Move(speed, transform);
    }

    private void SitAndSand(bool isSit)
    {
        Colliders[0].SetActive(!isSit);
        Colliders[1].SetActive(isSit);
    }
}
