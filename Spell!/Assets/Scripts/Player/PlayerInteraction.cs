using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private ItemList item;
    private ItemInteract pickedItem;

    [SerializeField] private EffectList effect;

    private PlayerInput input;
    private PlayerInventory inventory;
    [SerializeField] private GameObject potionEffect;
    private PotionEffect potionEffectScript;

    [Header("Item Position")]
    [SerializeField] private Transform handPosition;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        inventory = GetComponent<PlayerInventory>();

        potionEffectScript = potionEffect.GetComponent<PotionEffect>();

        pickedItem = null;
    }

    public void interact(InteractiveObject focusObject)
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (!UIManager.Instance.IsUIShown)
        {
            if (focusObject)
            {
                if (!PickOrMoveToInventoryItem(focusObject))
                {
                    InteractWithObejct(focusObject);
                }
            }
        }

        if(item != ItemList.NoItem)
        {
            UseItem();
            DropItem();
        }
    }

    private bool PickOrMoveToInventoryItem(InteractiveObject focusObject)
    {
        if (!input.Mouse0Click && !input.Mouse1Click)
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
        else if(input.Mouse1Click) // Mouse1Click
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
                SetPickedItem(pickedItem);
            }
        }
    }

    private void UseItem()
    {
        if(input.Mouse1Click)
        {
            pickedItem.Use(this);
            SetPickedItem(pickedItem);
        }
    }

    public void PickItem(ItemInteract item)
    {
        if (this.item != ItemList.NoItem &&
            !ItemToInventory(pickedItem))
        {
            return;
        }

        SetPickedItem(item);
        item.PickUp(handPosition);
    }

    public void DropItem()
    {
        if (UIManager.Instance.IsUIShown)
        {
            return;
        }

        if (input.Q)
        {
            pickedItem.DropDown();
            SetPickedItem();
        }
    }

    public void SelectItem(ItemInteract item, out ItemInteract preItem)
    {
        preItem = pickedItem;

        SetPickedItem(item);
        item.PickUp(handPosition);
    }

    public void SendPickedItemToInventory()
    {
        if (this.item != ItemList.NoItem)
        {
            inventory.AddItemToInventory(pickedItem);
        }
        SetPickedItem();
    }

    public void DestroyItem(ItemInteract item)
    {
        if (item == pickedItem)
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

    public void SetPlayerEffect(EffectList effect, Color newColor)
    {
        this.effect = effect;

        potionEffect.SetActive(effect != EffectList.NoEffect);
        potionEffectScript.SetPotionColor(newColor);
    }
}
