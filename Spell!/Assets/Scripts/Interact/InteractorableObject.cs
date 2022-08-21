using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;


public class InteractorableObject: MonoBehaviour, IInteractive
{
    private float lineWidth = 5f;
    private Outline outline;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactive");

        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = lineWidth;
        outline.enabled = false;
    }

    public void OnFocus()
    {
        Debug.Log($"{gameObject.name}: Focus");
        //outline.OutlineWidth = lineWidth;
        outline.enabled = true;
    }
    public void OutFocus()
    {
        Debug.Log($"{gameObject.name}: Out Focus");
        //outline.OutlineWidth = lineWidth;
        outline.enabled = false;
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name}: interact");

    }
}