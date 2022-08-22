using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;


public class InteractorableObject: MonoBehaviour, IInteractive
{
    [SerializeField] private Outline.Mode mode = Outline.Mode.OutlineVisible;
    private float lineWidth = 5f;
    private Outline outline;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();

        gameObject.layer = LayerMask.NameToLayer("Interactive");

        outline = gameObject.AddComponent<Outline>();
        if(outline == null)
        {
            outline = GetComponent<Outline>();
        }
        outline.OutlineMode = mode;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = lineWidth;
        outline.enabled = false;
    }

    public void OnFocus()
    {
        Debug.Log($"{gameObject.name}: Focus");
        outline.enabled = true;
        UIManager.Instance.SetInfoTextBar(gameObject.name.ToString());
    }
    public void OutFocus()
    {
        Debug.Log($"{gameObject.name}: Out Focus");
        outline.enabled = false;
        UIManager.Instance.SetInfoTextBar("");
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name}: interact");
        UIManager.Instance.SetInfoTextBar($"{gameObject.name}: interact");

    }
}