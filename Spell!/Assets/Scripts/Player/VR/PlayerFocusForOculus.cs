using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFocusForOculus : PlayerFocus
{
    [Header("Focus Object For Oculus")]
    [SerializeField] private Transform handPointer;
    [SerializeField] private Transform lineRendererPosition;
    private LineRenderer handPointerLineRenderer;

    [SerializeField] private float handReach = 50f;
    [SerializeField] private GameObject pointerSprite;

    private int layerMaskForUI;

    protected override void Awake()
    {
        base.Awake();

        pointerSprite.SetActive(false);

        handPointerLineRenderer = handPointer.GetComponent<LineRenderer>();

        LayerMask uiLayer = LayerMask.NameToLayer("UI");
        layerMaskForUI = (1 << uiLayer);
    }

    protected override void Update()
    {
        LineRendererSetting();
        UIButtonClick();
        base.Update();
    }

    protected void LineRendererSetting()
    {
        LineRendererSetting(lineRendererPosition.position + lineRendererPosition.forward * 2f);
    }
    protected void LineRendererSetting(Vector3 targetPosition)
    {
        Vector3[] linePosition = new Vector3[]
        {
            lineRendererPosition.position,
            targetPosition
        };
        handPointerLineRenderer.SetPositions(linePosition);
    }

    private void UIButtonClick()
    {
        if (!UIManager.Instance.IsUIShown)
        {
            return;
        }

        Ray ray = new Ray(handPointer.position, handPointer.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, handReach, layerMaskForUI))
        {
            Button hitButton = hit.transform.GetComponent<Button>();
            if (hitButton != null)
            {
                pointerSprite.SetActive(true);
                pointerSprite.transform.position = hit.point;
                LineRendererSetting(hit.point);

                if (input.Mouse0Click)
                {
                    hitButton.onClick.Invoke();
                }

                return;
            }
        }

        pointerSprite.SetActive(false);
    }

    protected override void FocusInteractiveObejct()
    {
        Ray ray = new Ray(handPointer.position, handPointer.forward);
        Focus(ray, handReach);
    }

    protected override void SetFocusObject(InteractiveObject item, RaycastHit hit)
    {
        base.SetFocusObject(item, hit);

        pointerSprite.transform.position = hit.point;
        pointerSprite.SetActive(true);
    }

    protected override void SetFocusObject()
    {
        base.SetFocusObject();

        pointerSprite.SetActive(false);
    }
}
