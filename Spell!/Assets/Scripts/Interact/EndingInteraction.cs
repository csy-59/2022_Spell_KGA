using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingInteraction : InteractiveObject
{
    [Header("Ending")]
    [SerializeField] private EndingList[] endingTypes;

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(!base.Interact(item, effect))
        {
            return false;
        }

        int endingNumber = base.InteractPreAssert(item, effect);

        if(endingNumber == -1)
        {
            UIManager.Instance.SetInfoTextBar("I don't think so...");
            return false;
        }

        UIManager.Instance.SetInfoTextBar("");
        GameManager.Instance.SetEnding(endingTypes[endingNumber]);
        return true;
    }
}
