using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class WallInteract : InteractiveObject
{
    [Header ("\nWall Interact")]
    [SerializeField] private int knockCount;
    [SerializeField] private float currentKnockCount;
    [SerializeField] private float knockTimeOffset = 1f;
    private float currentOffect;

    [SerializeField] private GameObject wallBreakEffect;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private float destroyTime = 1.75f;

    protected override void Awake()
    {
        base.Awake();
        
        wallBreakEffect.SetActive(false);
        foreach(GameObject wall in walls)
        {
            wall.SetActive(true);
        }
        
        currentKnockCount = knockCount;
    }

    void Update()
    {
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        if(base.InteractPreAssert(item,effect) == -1)
        {
            UIManager.Instance.SetInfoTextBar("It's wall. With more power, I think I can brake it.");
            return false;
        }

        --currentKnockCount;

        if (currentKnockCount > 0)
        {
            StartCoroutine(knockCountReset());
        }
        else
        {
            ShowHiddenItem();
        }

        return true;
    }

    private void ShowHiddenItem()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
        wallBreakEffect.SetActive(true);
        wallBreakEffect.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, destroyTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private IEnumerator knockCountReset()
    {
        currentOffect = knockTimeOffset;
        
        while(currentOffect > 0)
        {
            currentOffect -= Time.deltaTime;
            yield return null;
        }

        currentKnockCount = knockCount;
    }
}
