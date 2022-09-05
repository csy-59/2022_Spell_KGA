using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class ItemTorchInteract : ItemInteract
{
    [SerializeField] private GameObject fire;
    private bool isFireOn = false;

    protected override void Awake()
    {
        base.Awake();

        isFireOn = false;
        itemType = ItemList.Tourch;
    }

    private void OnEnable()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    public override void PickUp(Transform itemTransform)
    {
        base.PickUp(itemTransform);

        PlayerPrefsKey.SetCommonItemList((int)CommonItemList.Torch);
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(base.InteractPreAssertForItem(item, objectToInteract) != -1)
        {
            UIManager.Instance.SetInfoTextBar("Torch is on fire");
            isFireOn = true;
            fire.SetActive(true);
            itemType = ItemList.TourchWithFire;
            return true;
        }

        return false;
    }

    public override bool Use(PlayerInteraction player)
    {
        if(isFireOn)
        {
            isFireOn = false;
            fire.SetActive(false);
            itemType = ItemList.Tourch;
            return true;
        }

        return false;
    }
}
