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

    [SerializeField] protected EffectList[] necessaryEffect;
    [SerializeField] protected ItemList[] necessaryItem;

    protected virtual void Awake()
    {
        Reset(gameObject);
    }

    protected void Reset(GameObject target)
    {
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        AddOutline(target);
    }

    protected void AddOutline(GameObject target)
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
        if (InteractPreAssert(item, effect) != -1)
        {
            UIManager.Instance.SetInfoTextBar($"{gameObject.name}: interact");
        }
    }

    protected virtual int InteractPreAssert(ItemList item, EffectList effect)
    {
        for(int i = 0;i < necessaryEffect.Length; ++i)
        {
            if (necessaryEffect[i] != EffectList.DontCare && necessaryEffect[i] != effect)
            {
                continue;
            }

            if (necessaryItem[i] != ItemList.DontCare && necessaryItem[i] != item)
            {
                continue;
            }

            return i;
        }

        return -1;
    }
}