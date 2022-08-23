using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class LeverInteract : InteractiveObject
{
    [Header ("\nLeverInteract")]
    [SerializeField] private bool isLeverOn = false;
    [SerializeField] private float leverRotateAmount = 30f;
    [SerializeField] private float leverRotateSpeed = 200f;

    [SerializeField] private Transform lever;

    protected override void Awake()
    {
        base.Awake();

        if(isLeverOn)
        {
            lever.rotation *= Quaternion.Euler(leverRotateAmount, 0f, 0f);
        }
        else
        {
            lever.rotation *= Quaternion.Euler(-leverRotateAmount, 0f, 0f);
        }
    }

    void Update()
    {
        
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        LeverRotateHelper();
    }

    private void LeverRotateHelper()
    {
        if(isLeverOn)
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                lever,
                lever.rotation * Quaternion.Euler(-leverRotateAmount * 2, 0f, 0f),
                leverRotateSpeed, BeforeRotate, AfterRotate);
        }
        else
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                lever,
                lever.rotation * Quaternion.Euler(leverRotateAmount * 2, 0f, 0f),
                leverRotateSpeed, BeforeRotate, AfterRotate);
        }
    }
    private void BeforeRotate()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void AfterRotate()
    {
        isLeverOn = !isLeverOn;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
    }
}
