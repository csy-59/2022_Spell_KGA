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

    private PlayerInput input;
    private PlayerInventory inventory;

    [Header("Item Position")]
    [SerializeField] private Transform handPosition;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        inventory = GetComponent<PlayerInventory>();

        pickedItem = null;
    }

    public void Do(InteractiveObject focusObject)
    {
        if(focusObject)
        {
            if (!PickOrMoveToInventoryItem(focusObject))
            {
                InteractWithObejct(focusObject);
            }
        }

        if(item != ItemList.NoItem)
        {
            UseItem();
        }
    }

    private bool PickOrMoveToInventoryItem(InteractiveObject focusObject)
    {
        if(!input.Mouse0Click && !input.E)
        {
            return false;
        }

        if (focusObject.ObjectType != ObjectList.Item)
        {
            return false;
        }

        ItemInteract newItem = (ItemInteract)focusObject;

        if(input.Mouse0Click)
        {
            PickItem(newItem);
        }
        else // E
        {
            if(!ItemToInventory(newItem))
            {
                return false;
            }
        }

        return true;
    }

    private void InteractWithObejct(InteractiveObject focusObject)
    {
        if (input.Mouse0Click)
        {
            if(focusObject.Interact(item, effect) && item != ItemList.NoItem)
            {
                pickedItem.Interact(item, focusObject.ObjectType);
            }
        }
    }

    private void UseItem()
    {
        if(input.Mouse1Click && item != ItemList.NoItem)
        {
            pickedItem.Use();
        }
    }

    public void PickItem(ItemInteract item)
    {
        if (this.item != ItemList.NoItem)
        {
            inventory.AddItemToInventory(pickedItem);
        }
        SetPickedItem(item);
        item.PickUp(handPosition);
    }

    public void DropItem(ItemInteract item)
    {
        item.DropDown();
        SetPickedItem();
    }

    private bool ItemToInventory(ItemInteract item)
    {
        return inventory.AddItemToInventory(item);
    }

    private void SetPickedItem(ItemInteract item)
    {
        pickedItem = item;
        this.item = item.ItemType;
    }

    private void SetPickedItem()
    {
        pickedItem = null;
        this.item = ItemList.NoItem;
    }
}
