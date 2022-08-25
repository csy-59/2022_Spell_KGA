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

        tourchLight.SetActive(false);
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect) == -1)
        {
            return false;
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
                return false;
            }

            isTourchOn = true;
            TorchManager.Instance.LightOn(tourchNumber);
            UIManager.Instance.SetInfoTextBar("Torch is on fire");
        }

        tourchLight.SetActive(isTourchOn);
        return true;
    }
}
