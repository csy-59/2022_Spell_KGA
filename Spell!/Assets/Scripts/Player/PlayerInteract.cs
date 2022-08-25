using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private ItemList item;
    private ItemInteract pickedItem;

    [SerializeField] private EffectList effect;

    private PlayerFocus focus;
    private PlayerInput input;

    private void Awake()
    {
        focus = GetComponent<PlayerFocus>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        InteractWithObejct();
        UseItem();
    }

    private void InteractWithObejct()
    {
        if (input.Mouse0Click && focus.FocusObject != null)
        {
            if(focus.FocusObject.Interact(item, effect))
            {
                pickedItem?.Interact(item, focus.FocusObject.ObjectType);
            }
        }
    }

    private void UseItem()
    {
        if(input.Mouse1Click)
        {
            pickedItem?.Use();
        }
    }
}
