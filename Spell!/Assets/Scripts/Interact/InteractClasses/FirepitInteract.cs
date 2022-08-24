using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepitInteract : InteractiveObject
{
    [Header("Fierpit Interact")]
    [SerializeField] private GameObject fire;
    private bool isFireOn = false;

    [SerializeField] private GameObject fireScroll;
    private bool isScrollGiven = false;

    protected override void Awake()
    {
        base.Awake();

        fireScroll.SetActive(false);
        fire.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(isFireOn)
        {
            UIManager.Instance.SetInfoTextBar("The fire is warm");
            return;
        }

        if(base.InteractPreAssert(item, effect) != -1)
        {
            UIManager.Instance.SetInfoTextBar("I set the fire");
            isFireOn = true;
            fire.SetActive(true);
            return;
        }

        if(isScrollGiven)
        {
            UIManager.Instance.SetInfoTextBar("I need something to make fire");
            return;
        }

        UIManager.Instance.SetInfoTextBar("There was something in the ashes");
        fireScroll.SetActive(true);
        isScrollGiven = true;
    }
}
