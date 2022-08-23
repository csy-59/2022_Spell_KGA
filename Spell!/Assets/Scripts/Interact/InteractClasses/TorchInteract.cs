using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteract : InteractiveObject
{
    [Header("\nTourch Interaction")]
    [SerializeField] protected GameObject tourchLight;
    [SerializeField] private int tourchNumber;
    
    private bool isTourchOn;

    protected override void Awake()
    {
        base.Awake();

        //necessaryEffect = new EffectList[]
        //{
        //    EffectList.DontCare,
        //    EffectList.DontCare
        //};
        //necessaryItem = new ItemList[]
        //{
        //    ItemList.TourchWithFire,
        //    ItemList.DontCare
        //};

        tourchLight.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect) == -1)
        {
            return;
        }

        if(isTourchOn)
        {
            isTourchOn = false;
            TorchManager.Instance.LightOff(tourchNumber);
            UIManager.Instance.SetInfoTextBar("Torch went off");
        }
        else
        {
            if(!base.InteractPreAssert(item, effect, 0))
            {
                UIManager.Instance.SetInfoTextBar("It's dark");
                return;
            }

            isTourchOn = true;
            TorchManager.Instance.LightOn(tourchNumber);
            UIManager.Instance.SetInfoTextBar("Torch is on fire");
        }

        tourchLight.SetActive(isTourchOn);
    }
}
