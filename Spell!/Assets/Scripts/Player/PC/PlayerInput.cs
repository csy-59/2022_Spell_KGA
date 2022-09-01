using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float X { get; protected set; }
    public float Z { get; protected set; }

    public float MouseX { get; protected set; }
    public float MouseY { get; protected set; }
    public bool Mouse0Click { get; protected set; }
    public bool Mouse1Click { get; protected set; }

    public bool Shift { get; protected set; }
    public bool E { get; protected set; }
    public bool Q { get; protected set; }

    public bool AnyKey { get; protected set; }
    public bool Esc { get; protected set; }

    private void Update()
    {
        X = Input.GetAxis("Horizontal");
        Z = Input.GetAxis("Vertical");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        Mouse0Click = Input.GetMouseButtonDown(0);
        Mouse1Click = Input.GetMouseButtonDown(1);

        Shift = Input.GetKey(KeyCode.LeftShift);
        E = Input.GetKeyDown(KeyCode.E);
        Q = Input.GetKeyDown(KeyCode.Q);

        AnyKey = Input.anyKey;
        Esc = Input.GetKeyDown(KeyCode.Escape);
    }
}
