using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sitSpeed = 2f;

    public GameObject[] Colliders;

    private Rigidbody rigid;
    private PlayerInput input;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        SitAndSand(false);
    }

    private void FixedUpdate()
    {
        if(!UIManager.Instance.IsUIShown)
        {
            Move();
            SitAndSand(input.Shift);
        }
    }

    private void Move()
    {
        float speed = input.Shift ? sitSpeed : moveSpeed;

        Vector3 moveOffset = input.X * speed * Time.fixedDeltaTime * transform.right
            + input.Z * speed * Time.fixedDeltaTime * transform.forward;

        rigid.MovePosition(rigid.position + moveOffset);
    }
    private void SitAndSand(bool isSit)
    {
        Colliders[0].SetActive(!isSit);
        Colliders[1].SetActive(isSit);
    }
}
