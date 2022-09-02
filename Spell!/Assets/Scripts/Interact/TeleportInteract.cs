using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInteract : InteractiveObject
{
    [SerializeField] private Transform teleportTransform;
    private GameObject player;

    protected override void Awake()
    {
        base.Awake();

        PlayerInteraction[] players = FindObjectsOfType<PlayerInteraction>();
        foreach(var candidate in players)
        {
            if(candidate.gameObject.activeSelf)
            {
                player = candidate.gameObject;
            }
        }
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
