using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class SecretChestInteract : InteractiveObject
{
    [Header("Chest Dissolve")]
    private MeshRenderer[] meshRenderers = new MeshRenderer[2];
    [SerializeField] private float fadeSpeed = 0.5f;

    [SerializeField] private GameObject inChestItems;

    [Header("Chest Head")]
    [SerializeField] private GameObject chestHead;
    [SerializeField] private float chestHeadSpeed = 5f;
    private bool isChestOpen = false;

    protected override void Awake()
    {
        base.Awake();

        meshRenderers[0] = GetComponent<MeshRenderer>();
        meshRenderers[1] = chestHead.GetComponent<MeshRenderer>();
        meshRenderers[0].material.SetFloat("_Cutoff", 0f);
        meshRenderers[1].material.SetFloat("_Cutoff", 0f);

        inChestItems.SetActive(false);
    }

    private void OnEnable()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(ChestFadeIn());
        isChestOpen = false;

        chestHead.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private IEnumerator ChestFadeIn()
    {
        Material materialBody = meshRenderers[0].material;
        Material materialHead = meshRenderers[1].material;

        float dissolveValue = 1f;
        while (true)
        {
            dissolveValue = Mathf.Lerp(dissolveValue, 0f, fadeSpeed * Time.deltaTime);

            if (dissolveValue <= 0.1)
            {
                materialBody.SetFloat("_Cutoff", 0f);
                meshRenderers[0].material = materialBody;
                materialHead.SetFloat("_Cutoff", 0f);
                meshRenderers[1].material = materialHead;

                break;
            }
            else
            {
                materialBody.SetFloat("_Cutoff", dissolveValue);
                meshRenderers[0].material = materialBody;
                materialHead.SetFloat("_Cutoff", dissolveValue);
                meshRenderers[1].material = materialHead;
            }

            yield return null;
        }

        gameObject.layer = LayerMask.NameToLayer("Interactive");
        inChestItems.SetActive(true);
    }

    private void OnDisable()
    {
        meshRenderers[0].material.SetFloat("_Cutoff", 1f);
        meshRenderers[1].material.SetFloat("_Cutoff", 1f);
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(isChestOpen)
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                chestHead.transform,
                chestHead.transform.rotation * Quaternion.Euler(0f, 0f, 90f),
                chestHeadSpeed,
                new ObjectMove.BeforeService(() => { gameObject.layer = LayerMask.NameToLayer("Default"); }),
                new ObjectMove.AfterService(() => { gameObject.layer = LayerMask.NameToLayer("Interactive"); })
                );
        }
        else
        {
            ObjectMove.Instance.ObjectRotateToTargetRotation(
                chestHead.transform,
                chestHead.transform.rotation * Quaternion.Euler(0f, 0f, -90f),
                chestHeadSpeed,
                new ObjectMove.BeforeService(() => { gameObject.layer = LayerMask.NameToLayer("Default"); }),
                new ObjectMove.AfterService(() => { gameObject.layer = LayerMask.NameToLayer("Interactive"); })
                );
        }

        isChestOpen = !isChestOpen;
        return true;
    }

}
