using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class TitleCristalInteraction : TitleInteractiveObject
{
    [SerializeField] private CristalList cristalType;
    private int cristalNumber;

    // ȿ���� �߰� ����

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
        //ȿ���� �߰� ����
        return false;
    }


}
