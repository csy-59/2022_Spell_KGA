using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteract : InteractiveObject
{
    [Header ("\nLeverInteract")]
    [SerializeField] private bool isLeverOn = false;
    [SerializeField] private float leverRotateAmount = 30f;
    [SerializeField] private float leverRotateSpeed = 200f;

    [SerializeField] private Transform lever;

    protected override void Awake()
    {
        base.Awake();

        if(isLeverOn)
        {
            lever.rotation *= Quaternion.Euler(leverRotateAmount, 0f, 0f);
        }
        else
        {
            lever.rotation *= Quaternion.Euler(-leverRotateAmount, 0f, 0f);
        }
    }

    void Update()
    {
        
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        LeverRotateHelper();
    }

    private void LeverRotateHelper()
    {
        if(isLeverOn)
        {
            StartCoroutine(LeverRotate(-leverRotateAmount, -1));
        }
        else
        {
            StartCoroutine(LeverRotate(leverRotateAmount, 1));
        }
    }
    
    private IEnumerator LeverRotate(float endAngle, float offset)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");

        float currentAngle = -endAngle;
        while(true)
        {
            float deltaAngle = offset * leverRotateSpeed * Time.deltaTime;
            currentAngle += deltaAngle;

            lever.rotation *= Quaternion.Euler(deltaAngle, 0f, 0f);
            Debug.Log(lever.rotation.eulerAngles.x);

            if (Mathf.Abs(currentAngle) > Mathf.Abs(endAngle))
                break;

            yield return null;
        }

        isLeverOn = !isLeverOn;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
    }
}
