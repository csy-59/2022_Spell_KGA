using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class SecretButtonInteract : InteractiveObject
{
    [Header ("\nButton Interact")]
    [SerializeField] private Transform button;
    [SerializeField] private Vector3 buttonOffset = new Vector3( 0.04f, 0f, 0f);
    [SerializeField] private float buttonSpeed;
    [SerializeField] private GameObject chest;
    [SerializeField] private AudioClip buttonPushAudioClip;
    private AudioSource audioSource;


    protected override void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = buttonPushAudioClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        chest.SetActive(false);
    }

    public override void OnFocus() { }

    public override void OutFocus() { }

    public override bool Interact(ItemList item, EffectList effect)
    {
        ObjectMove.Instance.ObjectMoveToTargetPosition(
            button,
            button.position + buttonOffset,
            buttonSpeed,
            new ObjectMove.BeforeService(() => 
            { 
                gameObject.layer = LayerMask.NameToLayer("Default");
                audioSource.Play();
            }),
            new ObjectMove.AfterService(() => 
            { 
                chest.SetActive(true);
                audioSource.Pause();
            })
            );

        return true;
    }
}
