using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class RatHoleInteract : InteractiveObject
{
    [Header("\nRat Hole Interact")]
    [SerializeField] private GameObject key;
    [SerializeField] private float keySlideSpeed = 5;
    [SerializeField] private Transform keyPosition;

    private bool isKeyGiven = false;

    [Header("\nRat Hair Interact")]
    [SerializeField] private GameObject hair;
    [SerializeField] private float hairPickForce = 2f;
    private bool isHairGiven = false;

    protected override void Awake()
    {
        base.Awake();

        hair.SetActive(false);
    }

    void Update()
    {
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect, 1))
        {
            if(isHairGiven)
            {
                UIManager.Instance.SetInfoTextBar("I think i sould leave them alone...");
                return false;
            }

            UIManager.Instance.SetInfoTextBar("They almost bit me!");

            hair.SetActive(true);
            hair.transform.parent = null;
            hair.GetComponent<Rigidbody>().AddForce(Vector3.forward * hairPickForce, ForceMode.Impulse);

            isHairGiven = true;
            return true;
        }
        else
        {
            if (isKeyGiven)
            {
                UIManager.Instance.SetInfoTextBar("Rats seem happy");
                return false;
            }

            if(!base.InteractPreAssert(item, effect, 0))
            {
                UIManager.Instance.SetInfoTextBar("Rats seem hungry. They need Cheese...");
                return false;
            }

            RatReactAndGiveKey();
            return true;
        }

    }


    private void RatReactAndGiveKey()
    {
        UIManager.Instance.SetInfoTextBar("Rat is eating Cheese");

        key.SetActive(true);
        isKeyGiven = true;
        ObjectMove.Instance.ObjectMoveToTargetPosition(
            key.transform, 
            keyPosition.position, 
            keySlideSpeed,
            () => { key.GetComponent<Collider>().enabled = false; },
            () => { 
                key.GetComponent<Collider>().enabled = true;
                key.GetComponent<Rigidbody>().useGravity = true;
            }
            );
    }


}