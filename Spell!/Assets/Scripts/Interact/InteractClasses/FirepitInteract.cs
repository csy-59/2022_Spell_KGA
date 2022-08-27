using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepitInteract : WoodenPlankinteract
{
    [Header("Fierpit Interact")]
    [SerializeField] private GameObject fireScroll;
    private bool isScrollGiven = false;

    protected override void Awake()
    {
        base.Awake();

        fireScroll.SetActive(false);
        objectType = ObjectList.Firepit;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        if(base.InteractPreAssert(item, effect) != -1)
        {
            objectType = ObjectList.FirepitWithFire;
            return true;
        }

        if(isScrollGiven)
        {
            UIManager.Instance.SetInfoTextBar("I need something to make fire");
            return false;
        }

        UIManager.Instance.SetInfoTextBar("There was something in the ashes");
        fireScroll.SetActive(true);
        isScrollGiven = true;
        return true;
    }
}
