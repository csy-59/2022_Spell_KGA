using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class RatHoleInteract : InteractiveObject
{
    [Header("Wall Interact")]
    [SerializeField] private int knockCount;
    [SerializeField] private float knockTimeOffset = 1f;
    private float currentOffect;

    [SerializeField] private GameObject wallBreakEffect;
    [SerializeField] private GameObject wall;
    [SerializeField] private float destroyTime = 1.75f;

    public override void Awake()
    {
        base.Awake();
        wallBreakEffect.SetActive(false);
        wall.SetActive(true);
    }

    void Update()
    {
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        --knockCount;

        if (knockCount > 0)
        {
            StartCoroutine(knockCountReset());
            return;
        }

        RatReactAndGiveKey();
    }


    private void RatReactAndGiveKey()
    {
        wall.SetActive(false);
        wallBreakEffect.SetActive(true);
        wallBreakEffect.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, destroyTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private IEnumerator knockCountReset()
    {
        currentOffect = knockTimeOffset;

        while (currentOffect > 0)
        {
            currentOffect -= Time.deltaTime;
            yield return null;
        }

        knockCount = 0;
    }
}