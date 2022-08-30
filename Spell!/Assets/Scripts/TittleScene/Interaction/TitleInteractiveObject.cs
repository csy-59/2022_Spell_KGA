using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInteractiveObject : InteractiveObject
{
    [SerializeField] protected TitleUIManager uiManger;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnFocus()
    {
        outline.enabled = true;
        uiManger.SetInformationText(gameObject.name.ToString());
    }

    public override void OutFocus()
    {
        outline.enabled = false;
        uiManger.SetInformationText("");
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        return false;
    }
}
