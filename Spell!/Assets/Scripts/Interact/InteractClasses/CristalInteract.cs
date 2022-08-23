using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalInteract : InteractiveObject
{
    [Header ("\nCristal Interaction")]
    [SerializeField] private GameObject cristalShard;
    [SerializeField] private float popForce;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        int matchNumber;
        if(!base.InteractPreAssert(item, effect, out matchNumber))
        {
            return;
        }


    }
}
