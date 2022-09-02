using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPlankinteract : InteractiveObject
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject boilingEffect;
    public bool IsFireOn
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();

        fire.SetActive(false);
        boilingEffect.SetActive(false);
        objectType = ObjectList.WoodenPlank;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(IsFireOn)
        {
            UIManager.Instance.SetInfoTextBar("The fire is warm");
            if(base.InteractPreAssert(item, effect, 1))
            {
                return true;
            }
            return false;
        }

        if (base.Interact(item, effect))
        {
            UIManager.Instance.SetInfoTextBar("Water is boilling...");
            IsFireOn = true;
            fire.SetActive(true);
            boilingEffect.SetActive(true);
            objectType = ObjectList.WoodenPlankWithFire;
            return true;
        }

        UIManager.Instance.SetInfoTextBar("I need something to put on fire...");
        
        return false;
    }
}
