using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;
using InteractAsset;

public class TitleCommonItemInteraction : TitleInteractiveObject
{
    [SerializeField] private CommonItemList itemType;
    private int itemNumber;

    protected override void Awake()
    {
        base.Awake();

        itemNumber = (int)itemType;
        if(!TitleGameManger.Instance.IsCollactiveObejctCollected(PlayerPrefsKey.CommonItemKey, itemNumber))
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        return base.Interact(item, effect);
    }
}
