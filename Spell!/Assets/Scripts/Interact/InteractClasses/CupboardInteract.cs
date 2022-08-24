using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class CupboardInteract : InteractiveObject
{
    [Header("Cupboard Interact")]
    [SerializeField] private GameObject lockToShelf;
    private bool isLockOpen = false;

    [SerializeField] private Transform[] doors;
    [SerializeField] private float doorOpenYOffset = 90f;
    [SerializeField] private float doorSpeed;
    private bool isDoorOpen = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(isLockOpen)
        {
            if(isDoorOpen)
            {
                DoorOpen(doors[0], -doorOpenYOffset);
                DoorOpen(doors[1], doorOpenYOffset);
            }
            else
            {
                DoorOpen(doors[0], doorOpenYOffset);
                DoorOpen(doors[1], -doorOpenYOffset);
            }

            isDoorOpen = !isDoorOpen;
            return;
        }
        else
        {
            if(base.InteractPreAssert(item, effect) == -1)
            {
                UIManager.Instance.SetInfoTextBar("It has a lock. I need a key to open it");
                return;
            }

            UIManager.Instance.SetInfoTextBar("Lock opened");
            isLockOpen = true;
            lockToShelf.SetActive(false);
        }
    }

    private void DoorOpen(Transform door, float offset)
    {
        ObjectMove.Instance.ObjectRotateToTargetRotation(
            door,
            door.rotation * Quaternion.Euler(0f, offset, 0f),
            doorSpeed,
            gameObject
            );
    }
}
