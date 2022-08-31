using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusForTitle : MonoBehaviour
{
    [Header("Focus Obejct For General")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraReach = 1.8f;

    [Header("Focus Object For Oculus")]
    [SerializeField] private Transform handPointer;
    [SerializeField] private Transform lineRendererPosition;
    private LineRenderer handPointerLineRenderer;
    [SerializeField] private float handReach = 5f;
    
    private InteractiveObject focusObject;

    [SerializeField] private TitleUIManager uiManger;
    public InteractiveObject FocusObject
    {
        get => focusObject;
        private set
        {
            focusObject = value;
        }
    }

    private PlayerInput input;

    private int layerMask;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        if(!TitleGameManger.Instance.isNotOculus)
        {
            handPointerLineRenderer = handPointer.GetComponent<LineRenderer>();
        }

        // ∑π¿Ã ΩÓ±‚
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        layerMask = 1 << interactiveLayer;

        LayerMask itemLayer = LayerMask.NameToLayer("Item");
        layerMask = layerMask | (1 << itemLayer);

        LayerMask FloorLayer = LayerMask.NameToLayer("Floor");
        layerMask = layerMask | (1 << FloorLayer);
    }

    void Update()
    {
        if(!TitleGameManger.Instance.isNotOculus)
            LineRendererSetting();

        if (!uiManger.IsEndingScrollShown)
        {
            FocusSetting();
            Interact();
        }
        else
        {
            SetFocusObject();
        }
    }

    private void FocusSetting()
    {
        if(TitleGameManger.Instance.isNotOculus)
        {
            FocusInteractive();
        }
        else
        {
            FocusInteractiveForOculus();
        }
    }

    private void FocusInteractive()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        Focus(ray);
    }

    private void FocusInteractiveForOculus()
    {

        Ray ray = new Ray(handPointer.position, handPointer.forward);

        Focus(ray);
    }

    private void LineRendererSetting()
    {
        Vector3[] linePosition = new Vector3[]
        {
            lineRendererPosition.position,
            lineRendererPosition.position + lineRendererPosition.forward * 1.2f
        };
        handPointerLineRenderer.SetPositions(linePosition);
    }

    private void Focus(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, cameraReach, layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                SetFocusObject();
                return;
            }

            InteractiveObject nowFocusObejct = hit.transform.gameObject.GetComponent<InteractiveObject>();
            if (nowFocusObejct)
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

        if (focusObject)
        {
            focusObject.OnFocus();
        }
    }

    private void SetFocusObject()
    {
        SetFocusObject(null);
    }

    private void Interact()
    {
        if(input.Mouse0Click && focusObject)
        {
            focusObject.Interact(InteractAsset.ItemList.NoItem, 
                InteractAsset.EffectList.NoEffect);
        }
    }
}
