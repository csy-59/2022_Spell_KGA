using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementForOculus : PlayerMovement
{
    [Header("Move For Oculus")]
    [SerializeField] private float moveSpeedForOculus = .5f;
    [SerializeField] private float sitSpeedForOculus = .5f;


    [Header("Rotate")]
    [SerializeField] private float stickSensitivity = 10;
    [SerializeField] private Transform cameraTransform;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        if (!UIManager.Instance.IsUIShown)
        {
            Rotate();
        }
        base.FixedUpdate();
    }

    private void Rotate()
    {
        if (GameManager.Instance.IsNotOculus)
            return;

        PlayerInputForOculus oculusInput = (PlayerInputForOculus)input;

        float turnOffset = oculusInput.oculusCameraStick * stickSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * turnOffset);
    }

    protected override void Move()
    {
        Move(moveSpeedForOculus, cameraTransform);
    }
}
