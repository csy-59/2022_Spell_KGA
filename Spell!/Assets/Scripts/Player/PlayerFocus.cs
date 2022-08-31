using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InteractAsset;

public class PlayerFocus : MonoBehaviour
{
    [Header ("Focus Obejct")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float reach = 1.8f;

    [Header("Focus Object For Oculus")]
    [SerializeField] private Transform handPointer;
    [SerializeField] private Transform lineRendererPosition;
    private LineRenderer handPointerLineRenderer;
    [SerializeField] private float handReach = 5f;

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

        if (!GameManager.Instance.IsNotOculus)
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
        if (!GameManager.Instance.IsNotOculus)
        {
            LineRendererSetting();
        }

        UIButtonClick();
        FocusSetting();
        interaction.interact(focusObject);
    }

    private void LineRendererSetting()
    {
        Vector3[] linePosition = new Vector3[]
        {
            lineRendererPosition.position,
            lineRendererPosition.position + lineRendererPosition.forward * 2f
        };
        handPointerLineRenderer.SetPositions(linePosition);
    }

    private void FocusSetting()
    {
        if (UIManager.Instance.IsUIShown || GameManager.Instance.IsGameOver)
        {
            SetFocusObject();
            return;
        }

        if (GameManager.Instance.IsNotOculus)
        {
            FocusInteractive();
        }
        else
        {
            FocusInteractiveForOculus();
        }
    }

    private void Focus(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, reach, layerMask))
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

    private void UIButtonClick()
    {
        if (GameManager.Instance.IsNotOculus || UIManager.Instance.IsUIShown)
        {
            return;
        }

        Ray ray = new Ray(handPointer.position, handPointer.forward);

        LayerMask layerMask = LayerMask.NameToLayer("UI");
        int targetLayer = 1 << layerMask;

        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, reach, targetLayer))
        {
            Debug.Log(hit.transform.name);
            Button hitButton  = hit.transform.GetComponent<Button>();
            if(hitButton != null)
            {
                hitButton.onClick.Invoke();
            }
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
