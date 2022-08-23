using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class RatHoleInteract : InteractiveObject
{
    [Header("\nRat Hole Interact")]
    [SerializeField] private GameObject key;
    [SerializeField] private float keySlideSpeed = 5;
    [SerializeField] private Transform keyPosition;

    private bool isKeyGiven = false;

    protected override void Awake()
    {
        base.Awake();
    }

    void Update()
    {
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect) == -1)
        {
            UIManager.Instance.SetInfoTextBar("Rat seems hungry");
            return;
        }

        RatReactAndGiveKey();
    }


    private void RatReactAndGiveKey()
    {
        if(isKeyGiven)
        {
            UIManager.Instance.SetInfoTextBar("Rat seems happy");
            return;
        }

        UIManager.Instance.SetInfoTextBar("Rat is eating Cheese");

        key.SetActive(true);
        isKeyGiven = true;
        ObjectMove.Instance.ObjectMoveToTargetPosition(key.transform, keyPosition.position, keySlideSpeed);

        necessaryItem[0] = ItemList.DontCare;
    }


}