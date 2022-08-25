using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronInteract : InteractiveObject
{

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        return base.Interact(item, effect);
    }
}
