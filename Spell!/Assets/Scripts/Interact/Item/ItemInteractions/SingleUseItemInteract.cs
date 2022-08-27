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
        Debug.Log("helo");
        if (InteractPreAssertForItem(item, objectToInteract, 0))
        {
            Debug.Log("Destrory");
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
