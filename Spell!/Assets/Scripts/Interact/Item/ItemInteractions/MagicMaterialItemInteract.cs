using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class MagicMaterialItemInteract : ItemInteract
{
    [SerializeField] private MagicalMaterialList materialType;

    protected override void Awake()
    {
        base.Awake();

        necessaryItem = new ItemList[] { ItemList.DontCare, ItemList.DontCare };
        necessaryObject = new ObjectList[] { ObjectList.SmallCauldron, ObjectList.BigCauldron };
    }

    public override void PickUp(Transform itemTransform)
    {
        base.PickUp(itemTransform);

        PlayerPrefsKey.SetMagicalMaterialList((int)materialType);
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(InteractPreAssertForItem(item, objectToInteract) != -1)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
