using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class ItemInteract : MonoBehaviour
{
    [Header("Basic Item")]
    [SerializeField] private Outline.Mode mode = Outline.Mode.OutlineAll;
    private float lineWidth = 5f;
    private Outline outline;

    [SerializeField] private Sprite itemImage;
    [SerializeField] private float itemPickSize;
    private Vector3 originalSize;

    [SerializeField] protected readonly ItemList itemType;
    public ItemList ItemType
    {
        get => itemType;
    }

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Item");
        originalSize = gameObject.transform.localScale;

        AddOutline(gameObject);
    }

    private void AddOutline(GameObject target)
    {
        outline = target.AddComponent<Outline>();
        if (outline == null)
        {
            outline = target.GetComponent<Outline>();
        }
        outline.OutlineMode = mode;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = lineWidth;
        outline.enabled = false;
    }


}
