using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 200;
    [SerializeField] private float maxXRotation = 30f;
    [SerializeField] private float minXRotation = -80f;
    private float xRotation = 0f;

    private PlayerInput input;
    public Transform[] position;

    public Transform PlayerBody;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        input = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        if(!UIManager.Instance.IsUIShown)
        {
            CamaraRotateToCursor();
            SitAndStand();
        }
    }

    private void CamaraRotateToCursor()
    {
        float yOffset = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float xOffset = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= xOffset;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        PlayerBody.Rotate(Vector3.up * yOffset);
    }

    private void SitAndStand()
    {
        int positionNumber = input.Shift ? 1 : 0;
        transform.position = position[positionNumber].position;
    }
}
