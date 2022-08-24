using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInteract : InteractiveObject
{
    [SerializeField] private GameObject item;
    private bool isItemGiven = false;

    [SerializeField] private string getItemLine = "SomeThing dropped";
    [SerializeField] private string afterItemLine = "I think it's better not to touch him...";

    protected override void Awake()
    {
        base.Awake();

        item.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(isItemGiven)
        {
            UIManager.Instance.SetInfoTextBar(afterItemLine);
            return;
        }

        UIManager.Instance.SetInfoTextBar(getItemLine);
        this.item.SetActive(true);
        isItemGiven = true;
    }
}
