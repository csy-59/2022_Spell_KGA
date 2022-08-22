using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private InteractorableObject focusItem;

    private void Awake()
    {
    }

    void Update()
    {
        FocusInteractorable();
    }

    private void FocusInteractorable()
    {
        // ∑π¿Ã ΩÓ±‚
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        int layerMask = 1 << interactiveLayer;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 200f, layerMask))
        {
            InteractorableObject nowFocus = hit.transform.gameObject.GetComponent<InteractorableObject>();
            if(nowFocus)
            {
                if (focusItem == nowFocus)
                    return;


                setFocusItem(nowFocus);
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

    private void setFocusItem(InteractorableObject item)
    {
        focusItem?.OutFocus();
        focusItem = item;

        if(focusItem)
        {
            focusItem.OnFocus();
        }
    }

    private void setFocusItem()
    {
        setFocusItem(null);
    }
}
