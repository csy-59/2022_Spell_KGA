using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;

public class BrokenTorchInteract : TorchInteract
{
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject backWallEffect;
    [SerializeField] private float destroyTimeOffset = 1.7f;

    protected override void Awake()
    {
        base.Awake();

        backWall.SetActive(true);
        backWallEffect.SetActive(false);
    }

    public override void Interact(ItemList item, EffectList effect)
    {
        base.Interact(item, effect);

        if(base.InteractPreAssert(item, effect, 2))
        {
            UIManager.Instance.SetInfoTextBar("Wall has broken");
            gameObject.layer = LayerMask.NameToLayer("Default");

            tourchLight.SetActive(false);

            backWall.SetActive(false);
            backWallEffect.SetActive(true);
            backWallEffect.GetComponent<ParticleSystem>().Play();

            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, destroyTimeOffset);
        }
        else if(!base.InteractPreAssert(item, effect, 0))
        {
            UIManager.Instance.SetInfoTextBar("With more power, i think i can brake it");
        }
    }
}
