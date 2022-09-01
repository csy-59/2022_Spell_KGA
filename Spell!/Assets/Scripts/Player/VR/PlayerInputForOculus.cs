using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputForOculus : PlayerInput
{
    public float oculusCameraStick { get; private set; }

    private void Update()
    {
        SetXY();
        SetOculusCameraStick();
        SetMouseClick();
        SetOtherKey();
    }

    private void SetXY()
    {
        Vector2 coord = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        X = coord.x;
        Z = coord.y;
    }

    private void SetOculusCameraStick()
    {
        Vector2 stick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        oculusCameraStick = stick.x;
    }

    private void SetMouseClick()
    {
        Mouse0Click = OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger);
        Mouse1Click = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
    }

    private void SetOtherKey()
    {
        Shift = false;
        E = OVRInput.GetDown(OVRInput.Button.One);
        Q = OVRInput.GetDown(OVRInput.Button.Four);

        AnyKey = OVRInput.GetDown(OVRInput.Button.Any);
        Esc = OVRInput.GetDown(OVRInput.Button.Three);
    }
}
