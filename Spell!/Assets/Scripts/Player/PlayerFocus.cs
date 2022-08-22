using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerFocus : MonoBehaviour
{
    [Header ("Focus Obejct")]
    [SerializeField] private Camera camera;
    [SerializeField] private float reach = 1.8f;
    private InteractiveObject focusObject;

    [Header ("Player State")]
    [SerializeField] private ItemList item;
    [SerializeField] private EffectList effect;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        FocusInteractive();
        InteractWithObejct();
    }

    private void FocusInteractive()
    {
        // ∑π¿Ã ΩÓ±‚
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        int layerMask = 1 << interactiveLayer;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, reach, layerMask))
        {
            InteractiveObject nowFocusObejct = hit.transform.gameObject.GetComponent<InteractiveObject>();
            if(nowFocusObejct)
            {
                if (focusObject == nowFocusObejct)
                    return;


                SetFocusObject(nowFocusObejct);
            }
            else
            {
                setFocusItem();
            }
        }
        else
        {
            setFocusItem();
        }

    }

    private void InteractWithObejct()
    {
        if(input.MouseClick)
        {
            Debug.Log("Clicked");
            focusObject?.Interact(item, effect);
        }
    }

    private void SetFocusObject(InteractiveObject item)
    {
        focusObject?.OutFocus();
        focusObject = item;
        Debug.Log($"{focusObject.necessaryItem} {focusObject.necessaryEffect}");

        if(focusObject)
        {
            focusObject.OnFocus();
        }
    }

    private void SetFocusObject()
    {
        SetFocusObject(null);
    }
}
