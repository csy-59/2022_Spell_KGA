using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalInteract : InteractiveObject
{
    [Header ("\nCristal Interaction")]
    [SerializeField] private GameObject cristalShard;
    [SerializeField] private float popForce = 200;

    [SerializeField] private Transform shardPosition;
    [SerializeField] private Transform target;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        if(base.InteractPreAssert(item, effect) == -1)
        {
            return;
        }

        GameObject shard = Instantiate(cristalShard, shardPosition.position, shardPosition.rotation);
        shard.transform.LookAt(target);
        shard.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
    }
}
