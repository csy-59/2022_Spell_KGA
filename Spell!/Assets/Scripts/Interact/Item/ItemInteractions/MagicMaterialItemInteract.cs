using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMaterialItemInteract : ItemInteract
{
    protected override void Awake()
    {
        //itemPickSize = 1f;
        //itemPickRotation = new Vector3(0f, 0f, 0f);

        base.Awake();
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(base.Interact(item, objectToInteract))
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
