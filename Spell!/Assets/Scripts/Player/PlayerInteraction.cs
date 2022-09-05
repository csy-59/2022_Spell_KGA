using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Player State")]
    [SerializeField] private ItemList item;
    private ItemInteract pickedItem;
    private ItemInteract nullPickedItem;

    [SerializeField] private EffectList effect;

    private PlayerInput input;
    private PlayerInventory inventory;
    [SerializeField] private GameObject potionEffect;
    private PotionEffect potionEffectScript;

    [Header("Item Position")]
    [SerializeField] private Transform handPosition;

    [Header("Sound")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip itemPickupSound;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        inventory = GetComponent<PlayerInventory>();
        audioSource = GetComponent<AudioSource>();

        potionEffectScript = potionEffect.GetComponent<PotionEffect>();

        nullPickedItem = new ItemInteract();
        pickedItem = nullPickedItem;
        UIManager.Instance.SetPickedItem(pickedItem);
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
            UseItem(focusObject);
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

    private void UseItem(InteractiveObject focusObject)
    {
        if(input.Mouse1Click && !focusObject)
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
        PlaySound(itemPickupSound);
        item.PickUp(handPosition);
    }

    public void DropItem()
    {
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
        PlaySound(itemPickupSound);
        item.PickUp(handPosition);
    }

    public void SendPickedItemToInventory()
    {
        if (this.item != ItemList.NoItem)
        {
            inventory.AddItemToInventory(pickedItem);
        }
        SetPickedItem();
        PlaySound(itemPickupSound);
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
        UIManager.Instance.SetPickedItem(item);
    }

    private void SetPickedItem()
    {
        pickedItem = nullPickedItem;
        this.item = ItemList.NoItem;
        UIManager.Instance.SetPickedItem(nullPickedItem);
    }

    public void SetPlayerEffect(EffectList effect, Color newColor)
    {
        this.effect = effect;

        potionEffect.SetActive(effect != EffectList.NoEffect);
        potionEffectScript.SetPotionColor(newColor);
    }

    public void SetPlayerEffect()
    {
        this.effect = EffectList.NoEffect;

        potionEffect.SetActive(false);
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
