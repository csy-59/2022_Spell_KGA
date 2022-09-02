using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForTitle : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Rotate")]
    [SerializeField] private float stickSensitivity = 200;

    private Rigidbody rigid;
    private PlayerInput input;
    [SerializeField] private TitleUIManager uIManager;

    [SerializeField] private Transform cameraTransform;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (!TitleGameManger.Instance.isNotOculus)
        {
            PlayerInputForOculus oculusInput = (PlayerInputForOculus)input;

            float turnOffset = oculusInput.oculusCameraStick * stickSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * turnOffset);
        }
    }

    private void Move()
    {
        if (uIManager.IsEndingScrollShown)
        {
            return;
        }

        Vector3 moveOffset;
        if (TitleGameManger.Instance.isNotOculus)
        {
            moveOffset = input.X * moveSpeed * Time.fixedDeltaTime * transform.right
            + input.Z * moveSpeed * Time.fixedDeltaTime * transform.forward;
        }
        else
        {
            moveOffset = input.X * moveSpeed * Time.fixedDeltaTime * cameraTransform.right
            + input.Z * moveSpeed * Time.fixedDeltaTime * cameraTransform.forward;
        }

        rigid.MovePosition(rigid.position + moveOffset);
    }
}