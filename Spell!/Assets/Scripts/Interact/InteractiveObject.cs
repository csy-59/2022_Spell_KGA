using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;


public class InteractiveObject: MonoBehaviour
{
    [Header ("Basic Interactive")]
    [SerializeField] private Outline.Mode mode = Outline.Mode.OutlineVisible;
    private float lineWidth = 5f;
    private Outline outline;

    [SerializeField] protected EffectList necessaryEffect = EffectList.DontCare;
    [SerializeField] protected ItemList necessaryItem = ItemList.DontCare;

    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        outline = gameObject.AddComponent<Outline>();
        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }
        outline.OutlineMode = mode;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = lineWidth;
        outline.enabled = false;
    }

    public virtual void OnFocus()
    {
        outline.enabled = true;
        UIManager.Instance.SetInfoTextBar(gameObject.name.ToString());
    }
    public virtual void OutFocus()
    {
        outline.enabled = false;
        UIManager.Instance.SetInfoTextBar("");
    }

    public virtual void Interact(ItemList item, EffectList effect)
    {
        Debug.Log("Interact");
        if (InteractPreAssert(item, effect))
        {
            UIManager.Instance.SetInfoTextBar($"{gameObject.name}: interact");
        }
    }

    protected virtual bool InteractPreAssert(ItemList item, EffectList effect)
    {
        if (necessaryEffect != EffectList.DontCare && necessaryEffect != effect)
        {
            return false;
        }

        if (necessaryItem != ItemList.DontCare && necessaryItem != item)
        {
            return false;
        }

        return true;
    }
}