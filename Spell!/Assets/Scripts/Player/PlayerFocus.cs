using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    [SerializeField] public Camera camera;

    private InteractorableObject focusItem;

    private void Awake()
    {
        //camera = GetComponentInChildren<Camera>().GetComponent<Transform>();
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

                focusItem?.OutFocus();
                focusItem = nowFocus;
                focusItem.OnFocus();
            }
            else
            {
                focusItem.OutFocus();
                focusItem = null;
            }
            //BoxCollider collider = hit.transform.gameObject.GetComponent<BoxCollider>();
            //Debug.Assert(collider == null);
            //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            //InteractorableObject hitItem = hit.collider.gameObject.GetComponent<InteractorableObject>();
            //Debug.Assert(hitItem == null);
            //if(focusItem != null && focusItem == hitItem)
            //{
            //    return;
            //}
            //
            //focusItem.OutFocus();
            //focusItem = hitItem;
            //focusItem.OnFocus();
        }
        else
        {
            focusItem?.OutFocus();
            focusItem = null;
        }

    }
}
