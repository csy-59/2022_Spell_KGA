using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToCameraForTitle : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200;
    [SerializeField] private float maxXRotation = 30f;
    [SerializeField] private float minXRotation = -80f;
    private float xRotation = 0f;

    private PlayerInput input;

    public Transform PlayerBody;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        input = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        CamaraRotateToCursor();
    }

    private void CamaraRotateToCursor()
    {
        float yOffset = input.MouseX * mouseSensitivity * Time.deltaTime;
        float xOffset = input.MouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= xOffset;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * yOffset);
    }
}
