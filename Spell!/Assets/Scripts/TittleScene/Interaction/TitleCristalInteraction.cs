using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class TitleCristalInteraction : TitleInteractiveObject
{
    [SerializeField] private CristalList cristalType;
    private int cristalNumber;

    // 효과음 추가 예정

    protected override void Awake()
    {
        base.Awake();

        cristalNumber = (int)cristalType;
        if(!TitleGameManger.Instance.IsCollactiveObejctCollected(PlayerPrefsKey.CristalKey, cristalNumber))
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        //효과음 추가 예정
        return false;
    }


}
