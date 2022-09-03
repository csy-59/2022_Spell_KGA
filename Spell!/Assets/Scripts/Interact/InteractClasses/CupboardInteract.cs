using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class CupboardInteract : InteractiveObject
{
    [Header("Cupboard Interact")]
    [SerializeField] private GameObject lockToShelf;
    [SerializeField] private AudioClip unlockAudioClip;
    private AudioSource audioSource;
    private bool isLockOpen = false;

    [SerializeField] private Transform[] doors;
    [SerializeField] private float doorOpenYOffset = 90f;
    [SerializeField] private float doorSpeed;
    [SerializeField] private AudioClip openAndCloseClip;
    private bool isDoorOpen = false;
    private Collider doorCollider;

    protected override void Awake()
    {
        base.Awake();

        doorCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if (isLockOpen)
        {
            if (isDoorOpen)
            {
                DoorOpen(doors[0], -doorOpenYOffset);
                DoorOpen(doors[1], doorOpenYOffset);
                doorCollider.enabled = true;
                audioSource.PlayOneShot(openAndCloseClip);
            }
            else
            {
                DoorOpen(doors[0], doorOpenYOffset);
                DoorOpen(doors[1], -doorOpenYOffset);
                doorCollider.enabled = false;
                audioSource.PlayOneShot(openAndCloseClip);
            }

            isDoorOpen = !isDoorOpen;
            return true;
        }
        else
        {
            if (base.InteractPreAssert(item, effect) == -1)
            {
                UIManager.Instance.SetInfoTextBar("It has a lock. I need a key to open it");
                return false;
            }

            UIManager.Instance.SetInfoTextBar("Lock opened");
            audioSource.PlayOneShot(unlockAudioClip);
            isLockOpen = true;
            lockToShelf.SetActive(false);
            return true;
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
