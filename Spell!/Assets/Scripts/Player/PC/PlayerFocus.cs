using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerFocus : MonoBehaviour
{
    [Header ("Focus Obejct")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraReach = 1.8f;

    protected InteractiveObject focusObject;
    public InteractiveObject FocusObject 
    { get => focusObject; private set { focusObject = value; } }

    protected PlayerInput input;
    protected PlayerInteraction interaction;

    private int layerMask;

    protected virtual void Awake()
    {
        input = GetComponent<PlayerInput>();
        interaction = GetComponent<PlayerInteraction>();

        // ∑π¿Ã ΩÓ±‚
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        layerMask = 1 << interactiveLayer;

        LayerMask itemLayer = LayerMask.NameToLayer("Item");
        layerMask = layerMask | (1 << itemLayer);

        LayerMask FloorLayer = LayerMask.NameToLayer("Floor");
        layerMask = layerMask | (1 << FloorLayer);
    }

    protected virtual void Update()
    {
        FocusSetting();
        interaction.interact(focusObject);
    }

    private void FocusSetting()
    {
        if (UIManager.Instance.IsUIShown || GameManager.Instance.IsGameOver)
        {
            SetFocusObject();
            return;
        }

        FocusInteractiveObejct();
    }
    protected virtual void FocusInteractiveObejct()
    {
        Vector3 screenCenter = new Vector3
        (playerCamera.pixelWidth/2, playerCamera.pixelHeight/2);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
        Focus(ray, cameraReach);
    }
    protected void Focus(Ray ray, float reach)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, reach, layerMask))
        {
            InteractiveObject nowFocusObejct = hit.transform.gameObject.GetComponent<InteractiveObject>();
            if (nowFocusObejct)
            {
                if (focusObject == nowFocusObejct)
                    return;

                SetFocusObject(nowFocusObejct, hit);
            }
            else
            {
                SetFocusObject();
            }
        }
        else
        {
            SetFocusObject();
        }
    }

    protected virtual void SetFocusObject(InteractiveObject item, RaycastHit hit)
    {
        focusObject?.OutFocus();
        focusObject = item;

        if(focusObject)
        {
            focusObject.OnFocus();
        }
    }
    protected virtual void SetFocusObject()
    {
        SetFocusObject(null, new RaycastHit());
    }
}
