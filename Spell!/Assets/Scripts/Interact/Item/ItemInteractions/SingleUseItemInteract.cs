using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class SingleUseItemInteract : ItemInteract
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if (InteractPreAssertForItem(item, objectToInteract, 0))
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
