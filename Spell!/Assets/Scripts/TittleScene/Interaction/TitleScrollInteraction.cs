using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class TitleScrollInteraction : TitleInteractiveObject
{
    [SerializeField] private ScrollList scrollType;
    private int scrollNumber;
    [SerializeField] private Sprite scrollSprite;

    protected override void Awake()
    {
        base.Awake();

        scrollNumber = (int)scrollType;

        if(!TitleGameManger.Instance.IsCollactiveObejctCollected(PlayerPrefsKey.ScrollKey, scrollNumber))
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        uiManger.ShowScroll(scrollSprite);
        return false;
    }
}
