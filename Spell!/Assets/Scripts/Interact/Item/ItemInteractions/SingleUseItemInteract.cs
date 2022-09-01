using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class SingleUseItemInteract : ItemInteract
{
    [SerializeField] private CommonItemList commonItemType;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void PickUp(Transform itemTransform)
    {
        base.PickUp(itemTransform);
        PlayerPrefsKey.SetCommonItemList((int)commonItemType);
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
