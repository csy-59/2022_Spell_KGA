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
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction, Color.green, 100f);
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 200f))
        {
            Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.gameObject.layer != interactiveLayer.value)
            {
                focusItem?.OutFocus();
                focusItem = null;
                return;
            }

            hit.transform.gameObject.SetActive(false);
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

    }
}
