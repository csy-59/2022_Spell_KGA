using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : MonoBehaviour
{
    private InteractorableObject focusItem;

    void Update()
    {
        
    }

    private void FocusInteractorable()
    {
        // ���� ���
        LayerMask interactiveLayer = LayerMask.NameToLayer("Interactive");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {

        }

    }
}
