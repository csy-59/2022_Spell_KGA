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

    [Header("\nHiddenFloor")]
    [SerializeField] private Transform tile;
    [SerializeField] private Vector3 tileMoveOffset = new Vector3(0f, 0f, -1.9f);
    [SerializeField] private float tileMoveSpeed = 5f;
    [SerializeField] private Transform ladder;
    [SerializeField] private Transform ladderMovePosition;
    [SerializeField] private float ladderMoveSpeed = 5f;

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

    public override bool Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        LeverRotateHelper();
        return true;
    }

    private void LeverRotateHelper()
    {
        if(isLeverOn)
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                lever,
                lever.rotation * Quaternion.Euler(-leverRotateAmount * 2, 0f, 0f),
                leverRotateSpeed, BeforeLeverRotate, AfterLeverRotate);
        }
        else
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                lever,
                lever.rotation * Quaternion.Euler(leverRotateAmount * 2, 0f, 0f),
                leverRotateSpeed, BeforeLeverRotate, AfterLeverRotate);
        }
    }
    private void BeforeLeverRotate()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void AfterLeverRotate()
    {
        isLeverOn = !isLeverOn;
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        ObjectMove.Instance.ObjectMoveToTargetPosition(
            tile,
            tile.position + tileMoveOffset,
            tileMoveSpeed, null, AfterTileMove);
    }

    private void AfterTileMove()
    {
        ObjectMove.Instance.ObjectMoveToTargetPosition(
            ladder,
            ladderMovePosition.position,
            ladderMoveSpeed);
    }
}
