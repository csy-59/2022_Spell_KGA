using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class TorchManager : SingletonBehaviour<TorchManager>
{
    private int lightBit = 0;
    [SerializeField] private int[] secretPathLightNumber = { 4, 9 };
    [SerializeField] private bool isSecretOpen = false;
    private int secretLightMask;

    [SerializeField] private Transform secretTile;
    [SerializeField] private Vector3 tileOffset;
    [SerializeField] private float tileSpeed;

    private void Awake()
    {
        foreach (int num in secretPathLightNumber)
        {
            secretLightMask = secretLightMask | (1 << num);
        }
    }

    void Update()
    {

    }

    public void LightOn(int lightNumber)
    {
        lightBit = lightBit | (1 << lightNumber);
        LightCheck();
    }

    public void LightOff(int lightNumber)
    {
        lightBit = lightBit & ~(1 << lightNumber);
        LightCheck();
    }

    private void LightCheck()
    {
        if (isSecretOpen)
            return;

        if (lightBit == secretLightMask)
        {
            isSecretOpen = true;
            ObjectMove.Instance.ObjectMoveToTargetPosition(
                secretTile,
                secretTile.position + tileOffset,
                tileSpeed);
        }
    }
}
