using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sitSpeed = 2f;

    [Header("Rotate")]
    [SerializeField] private float stickSensitivity = 200;
    [SerializeField] private Transform cameraTransform;

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
            Rotate();
            Move();
            SitAndSand(input.Shift);
        }
    }

    private void Move()
    {
        float speed = input.Shift ? sitSpeed : moveSpeed;

        Vector3 moveOffset;
        if (TitleGameManger.Instance.isNotOculus)
        {
            moveOffset = input.X * speed * Time.fixedDeltaTime * transform.right
            + input.Z * speed * Time.fixedDeltaTime * transform.forward;
        }
        else
        {
            moveOffset = input.X * speed * Time.fixedDeltaTime * cameraTransform.right
            + input.Z * speed * Time.fixedDeltaTime * cameraTransform.forward;
        }
        
        rigid.MovePosition(rigid.position + moveOffset);
    }

    private void Rotate()
    {
        if(!GameManager.Instance.IsNotOculus)
        {
            PlayerInputForOculus oculusInput = (PlayerInputForOculus)input;

            float turnOffset = oculusInput.oculusCameraStick * stickSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * turnOffset);
        }
    }

    private void SitAndSand(bool isSit)
    {
        Colliders[0].SetActive(!isSit);
        Colliders[1].SetActive(isSit);
    }
}
