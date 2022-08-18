using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;


public class InteractorableObject: MonoBehaviour, IInteractive
{
    [SerializeField] private float lineWidth = 3.5f;
    private Outline outline;

    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 0f;
    }

    public void OnFocus()
    {
        Debug.Log($"{gameObject.name}: Focus");
        outline.OutlineWidth = lineWidth;
    }
    public void OutFocus()
    {
        Debug.Log($"{gameObject.name}: Out Focus");
        outline.OutlineWidth = 0f;
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name}: interact");

    }
}