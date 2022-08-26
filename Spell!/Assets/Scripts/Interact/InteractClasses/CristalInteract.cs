using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalInteract : InteractiveObject
{
    [Header ("\nCristal Interaction")]
    [SerializeField] private GameObject cristalShard;
    [SerializeField] private float popForce = 3;

    [SerializeField] private Transform shardPosition;
    [SerializeField] private Transform target;

    [SerializeField] private float shardGenerateOffsetTime = 15f;
    private float lastShardGenerateTime;

    protected override void Awake()
    {
        base.Awake();

        lastShardGenerateTime = Time.time;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(Time.time - lastShardGenerateTime < shardGenerateOffsetTime)
        {
            UIManager.Instance.SetInfoTextBar("Cristal is not ready...");
            return false;
        }

        lastShardGenerateTime = Time.time;

        GameObject shard = Instantiate(cristalShard, shardPosition.position, shardPosition.rotation);
        shard.name = $"{cristalShard}";
        shard.transform.LookAt(target);
        shard.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
        return true;
    }
}
