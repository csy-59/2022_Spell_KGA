using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class BirdInteract : InteractiveObject
{
    [Header ("Bird Interact")]
    [SerializeField] private GameObject hintScroll;
    private bool isHintGiven = false;

    [Header ("Feather Interact")]
    [SerializeField] private GameObject feather;
    [SerializeField] private Transform featherPosition;
    [SerializeField] private int featherPickedCount;

    [SerializeField] private Transform awayPoint;
    [SerializeField] private float flySpeed;

    protected override void Awake()
    {
        base.Awake();

        hintScroll.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(isHintGiven)
        {
            UIManager.Instance.SetInfoTextBar("Bird seems happy");
            return;
        }

        if(base.InteractPreAssert(item, effect, 0))
        {
            UIManager.Instance.SetInfoTextBar("The Bird is staring at me. He seems hungry");

            return;
        }

        if(base.InteractPreAssert(item, effect, 1))
        {
            if(featherPickedCount > 0)
            {
                UIManager.Instance.SetInfoTextBar("The Bird is annoyed because of the lost feather");
                --featherPickedCount;
                Instantiate(feather, featherPosition);
                return;
            }

            UIManager.Instance.SetInfoTextBar("The Bird left");
            FlyAway();
            return;
        }

        UIManager.Instance.SetInfoTextBar("The Bird is eating meat.");
        hintScroll.SetActive(true);
        isHintGiven = true;
    }

    private void FlyAway()
    {

    }
}
