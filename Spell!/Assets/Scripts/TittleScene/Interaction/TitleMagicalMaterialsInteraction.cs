using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class TitleMagicalMaterialsInteraction : TitleInteractiveObject
{
    [SerializeField] private MagicalMaterialList materialType;
    private int materialNumber;

    protected override void Awake()
    {
        base.Awake();

        materialNumber = (int)materialType;
        if(!TitleGameManger.Instance.IsCollactiveObejctCollected(PlayerPrefsKey.MagicMaterialKey, materialNumber))
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        return false;
    }
}
