using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class CristalInteract : InteractiveObject
{
    [Header ("\nCristal Interaction")]
    [SerializeField] private GameObject cristalShard;
    [SerializeField] private CristalList cristalType;
    [SerializeField] private float popForce = 3;
    private AudioSource audioSource;
    [SerializeField] private AudioClip giveShardAudioClip;

    [SerializeField] private Transform shardPosition;

    [SerializeField] private float shardGenerateOffsetTime = 15f;
    private float lastShardGenerateTime;

    protected override void Awake()
    {
        base.Awake();

        lastShardGenerateTime = Time.time;
        audioSource = gameObject.AddComponent<AudioSource>();
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
        shard.name = cristalShard.name;
        shard.GetComponent<Rigidbody>().AddForce(Vector3.up * popForce, ForceMode.Impulse);
        audioSource.PlayOneShot(giveShardAudioClip);

        PlayerPrefsKey.SetCristalList((int)cristalType);

        return true;
    }
}
