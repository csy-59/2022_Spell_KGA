using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusForTitleForOculus : PlayerFocusForOculus
{
    [SerializeField] private TitleUIManager titleUIManager;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        LineRendererSetting();
        FocusSetting();
        Interact();
    }

    protected override void FocusSetting()
    {
        if (titleUIManager.IsEndingScrollShown)
        {
            SetFocusObject();
            return;
        }

        FocusInteractiveObejct();
    }
    private void Interact()
    {
        if (input.Mouse0Click && focusObject)
        {
            focusObject.Interact(InteractAsset.ItemList.NoItem,
                InteractAsset.EffectList.NoEffect);
        }
    }
}
