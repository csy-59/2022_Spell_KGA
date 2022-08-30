using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;
using InteractAsset;

public class ScrollInteract : ItemInteract
{
    [SerializeField] private ScrollList scrollType;
    [SerializeField] private Sprite scrollSprite;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnFocus()
    {
        base.OnFocus();
        UIManager.Instance.SetInfoTextBar("Old Scroll");
    }

    public override bool Use(PlayerInteraction player)
    {
        UIManager.Instance.ShowScroll(scrollSprite);

        PlayerPrefsKey.SetScrollList((int)scrollType);

        return true;
    }
}
