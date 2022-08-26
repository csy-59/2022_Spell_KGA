using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInteract : InteractiveObject
{
    [SerializeField] private Transform teleportTransform;
    [SerializeField] private GameObject player;

    protected override void Awake()
    {
        base.Awake();
    }

    //private void PropSettings(bool isActive)
    //{
    //    foreach (GameObject props in propsToShow)
    //    {
    //        props.SetActive(false);
    //    }
    //}

    public override bool Interact(ItemList item, EffectList effect)
    {
        UIManager.Instance.BlackOut(10f, BeforeBlackOut, AfterBlackOut);
        return false;
    }

    private void BeforeBlackOut()
    {

    }

    private void AfterBlackOut()
    {
        player.transform.localPosition = teleportTransform.position;
        player.transform.localRotation = teleportTransform.rotation;
    }
}
