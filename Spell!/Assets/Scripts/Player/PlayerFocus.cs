using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class PlayerFocus : MonoBehaviour
{
    [Header ("Focus Obejct")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float reach = 1.8f;
    private InteractiveObject focusObject;
    public InteractiveObject FocusObject
    {
        get => focusObject;
        private set 
        {
            focusObject = value;
        }
    }

    private PlayerInput input;
    private PlayerInteraction interaction;

    private int layerMask;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        interaction = GetComponent<PlayerInteraction>();


        // ���� ���
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        layerMask = 1 << interactiveLayer;

        LayerMask itemLayer = LayerMask.NameToLayer("Item");
        layerMask = layerMask | (1 << itemLayer);

        LayerMask FloorLayer = LayerMask.NameToLayer("Floor");
        layerMask = layerMask | (1 << FloorLayer);
    }

    void Update()
    {
        FocusInteractive();
        interaction.interact(focusObject);
    }

    private void FocusInteractive()
    {
        if (UIManager.Instance.IsUIShown)
        {
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, reach, layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                SetFocusObject();
                return;
            }

            InteractiveObject nowFocusObejct = hit.transform.gameObject.GetComponent<InteractiveObject>();
            if(nowFocusObejct)
            {
                if (focusObject == nowFocusObejct)
                    return;


                SetFocusObject(nowFocusObejct);
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

    private void SetFocusObject(InteractiveObject item)
    {
        focusObject?.OutFocus();
        focusObject = item;

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
