using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInteract : InteractiveObject
{
    [Header ("\nSkeleton Item Interaction")]
    [SerializeField] private GameObject item;
    [SerializeField] private string getItemLine = "SomeThing dropped";
    [SerializeField] private string afterItemLine = "I think it's better not to touch him...";
    private bool isItemGiven = false;

    [Header ("\nSkeleton bone Interaction")]
    [SerializeField] private GameObject bone;
    [SerializeField] private Transform bonePosition;
    [SerializeField] private string getBoneLine = "He's already dead, right?";
    [SerializeField] private string afterBoneLine = "Sorry for that...";
    private bool isBoneGiven = false;

    protected override void Awake()
    {
        base.Awake();

        necessaryEffect = new EffectList[]
        {
            EffectList.DontCare,
            EffectList.Power
        };
        necessaryItem = new ItemList[]
        {
            ItemList.DontCare,
            ItemList.DontCare
        };

        item.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect, 1))
        {
            if(isBoneGiven)
            {
                UIManager.Instance.SetInfoTextBar(afterBoneLine);
                return;
            }

            UIManager.Instance.SetInfoTextBar(getBoneLine);
            Instantiate(bone, bonePosition.position, bonePosition.rotation);
            isBoneGiven = true;
        }
        else
        {
            if (isItemGiven)
            {
                UIManager.Instance.SetInfoTextBar(afterItemLine);
                return;
            }

            UIManager.Instance.SetInfoTextBar(getItemLine);
            this.item.SetActive(true);
            isItemGiven = true;
        }
    }
}
