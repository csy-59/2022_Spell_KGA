using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class ItemInteract : InteractiveObject
{
    [SerializeField] protected ObjectList[] necessaryObject  = { ObjectList.DontCare };

    [Header("Basic Item")]
    [SerializeField] private Sprite itemImage;
    [SerializeField] protected ItemList itemType = ItemList.NoItem;

    [SerializeField] private float itemPickSize;
    [SerializeField] private Vector3 itemPickRotation;
    private Vector3 pickSize;
    private Vector3 originalSize;

    private bool isItemPicked = false;

    private Rigidbody rigid;
    private Collider[] colliders;
    private Collider[] collidersInChild;

    protected static readonly Vector3 zeroVector = Vector3.zero;

    public ItemList ItemType
    {
        get => itemType;
    }

    protected override void Awake()
    {
        base.Awake();

        ObjectType = ObjectList.Item;
        gameObject.layer = LayerMask.NameToLayer("Item");

        pickSize = new Vector3(itemPickSize, itemPickSize, itemPickSize);
        originalSize = gameObject.transform.localScale;

        rigid = GetComponent<Rigidbody>();
        colliders = GetComponents<Collider>();
        collidersInChild = GetComponentsInChildren<Collider>();
    }

    private void Update()
    {
        if(isItemPicked)
            transform.localPosition = zeroVector;    
    }

    public virtual bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(InteractPreAssertForItem(item, objectToInteract) != -1)
        {
            return true;
        }

        return false;
    }

    protected int InteractPreAssertForItem(ItemList item, ObjectList objectToInteract)
    {
        for (int i = 0; i < necessaryEffect.Length; ++i)
        {
            if (InteractPreAssertForItem(item, objectToInteract, i))
                return i;
        }

        return -1;
    }

    protected bool InteractPreAssertForItem(ItemList item, ObjectList objectToInteract, int assertNumber)
    {
        if (assertNumber < 0 || assertNumber >= necessaryItem.Length)
        {
            return false;
        }

        if (necessaryItem[assertNumber] != ItemList.DontCare
            && necessaryItem[assertNumber] != item)
        {
            return false;
        }

        if (necessaryObject[assertNumber] != ObjectList.DontCare
            && necessaryObject[assertNumber] != objectToInteract)
        {
            return false;
        }

        return true;
    }

    public virtual void PickUp(Transform itemPosition)
    {
        itemSetting(pickSize, false);
        gameObject.layer = LayerMask.NameToLayer("PickedItem");
        rigid.velocity = zeroVector;

        transform.rotation = itemPosition.rotation * Quaternion.Euler(itemPickRotation);
        transform.parent = itemPosition;
        transform.localPosition = zeroVector;

        gameObject.SetActive(true);
    }

    public virtual void ToInventory(Transform itemPosition)
    {
        if(!isItemPicked)
        {
            PickUp(itemPosition);
        }

        transform.parent = itemPosition;
        transform.localPosition = zeroVector;

        gameObject.SetActive(false);
    }

    public virtual void DropDown()
    {
        itemSetting(originalSize, true);
        gameObject.transform.parent = null;
        gameObject.layer = LayerMask.NameToLayer("Item");
        gameObject.SetActive(true);
    }

    private void itemSetting(Vector3 scale, bool isDropped)
    {
        gameObject.transform.localScale = scale;

        rigid.useGravity = isDropped;
        foreach(Collider collider in colliders)
        {
            collider.enabled = isDropped;
        }
        foreach(Collider collider in collidersInChild)
        {
            collider.enabled = isDropped;
        }

        isItemPicked = !isDropped;
    }

    public virtual bool Use()
    {
        return false;
    }
}
