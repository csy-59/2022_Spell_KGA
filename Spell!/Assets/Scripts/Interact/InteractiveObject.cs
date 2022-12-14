using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;


public class InteractiveObject: MonoBehaviour
{
    [Header ("Basic Interactive")]
    [SerializeField] private Outline.Mode mode = Outline.Mode.OutlineVisible;
    private float lineWidth = 5f;
    protected Outline outline;
    [SerializeField] private string interactFailLine = "interact";

    [SerializeField] protected ObjectList objectType;
    public ObjectList ObjectType
    {
        get => objectType;
        protected set
        {
            objectType = value;
        }
    }

    [SerializeField] protected EffectList[] necessaryEffect = { EffectList.DontCare };
    [SerializeField] protected ItemList[] necessaryItem = { ItemList.DontCare };

    protected virtual void Awake()
    {
        Reset(gameObject);
    }

    protected void Reset(GameObject target)
    {
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        AddOutline(target);
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

    public virtual void OnFocus()
    {
        outline.enabled = true;
        UIManager.Instance.SetNameTextBar(gameObject.name.ToString());
    }
    public virtual void OutFocus()
    {
        outline.enabled = false;
        UIManager.Instance.SetNameTextBar("");
        UIManager.Instance.SetInfoTextBar("");
    }

    public virtual bool Interact(ItemList item, EffectList effect)
    {
        if (InteractPreAssert(item, effect) == -1)
        {
            UIManager.Instance.SetInfoTextBar(interactFailLine);
            return false;
        }

        return true;
    }

    protected int InteractPreAssert(ItemList item, EffectList effect)
    {
        for(int i = 0;i < necessaryEffect.Length; ++i)
        {
            if(InteractPreAssert(item, effect, i))
                return i;
        }

        return -1;
    }

    protected bool InteractPreAssert(ItemList item, EffectList effect, int assertNumber)
    {
        if(assertNumber < 0 || assertNumber >= necessaryEffect.Length)
        {
            return false;
        }

        if (necessaryEffect[assertNumber] != EffectList.DontCare 
            && necessaryEffect[assertNumber] != effect)
        {
            return false;
        }

        if (necessaryItem[assertNumber] != ItemList.DontCare 
            && necessaryItem[assertNumber] != item)
        {
            return false;
        }

        return true;
    }
}