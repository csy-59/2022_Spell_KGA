using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMaterialItemInteract : ItemInteract
{
    protected override void Awake()
    {
        base.Awake();

        necessaryItem = new ItemList[] { ItemList.DontCare, ItemList.DontCare };
        necessaryObject = new ObjectList[] { ObjectList.SmallCauldron, ObjectList.BigCauldron };
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(InteractPreAssertForItem(item, objectToInteract, 0))
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
