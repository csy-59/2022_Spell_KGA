using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class TitleBoardInteraction : InteractiveObject
{
    [Header("EndingBoard")]
    [SerializeField] private EndingList endingType;
    [SerializeField] private GameObject endingCollectedItem;
    [SerializeField] private TitleUIManager uiManger;
    private int endingNumber;
    private bool isEndingCollacted = false;

    protected override void Awake()
    {
        base.Awake();

        endingNumber = (int)endingType;
        isEndingCollacted = PlayerPrefsKey.IsEndingCollacted(endingNumber);

        if (!isEndingCollacted)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            endingCollectedItem.SetActive(false);
        }
        else
        {
            endingCollectedItem.SetActive(true);
        }
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OutFocus()
    {
        outline.enabled = false;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        uiManger.ShowEndingScroll(endingNumber);
        return false;
    }
}
