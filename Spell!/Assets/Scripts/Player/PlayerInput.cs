using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float X { get; private set; }
    public float Z { get; private set; }

    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    public bool Shift { get; private set; }
    private void FixedUpdate()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Shift = Input.GetKey(KeyCode.LeftShift);
    }
}
