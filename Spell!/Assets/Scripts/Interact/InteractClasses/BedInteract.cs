using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class BedInteract : InteractiveObject
{
    [Header("Bed Interaction")]
    [SerializeField] private GameObject brakeEffect;
    [SerializeField] private int punchCount = 5;

    private Animator punchEffect;

    protected override void Awake()
    {
        base.Awake();

        punchEffect = GetComponent<Animator>();

        brakeEffect.SetActive(false);
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(base.InteractPreAssert(item, effect, 0))
        {
            --punchCount;
            if(punchCount > 0)
            {
                UIManager.Instance.SetInfoTextBar("More Punch!!");
                punchEffect.SetTrigger(AnimationID.Bed_Punch);
                return true;
            }

            UIManager.Instance.SetInfoTextBar("I did it!");
            
            brakeEffect.SetActive(true);
            brakeEffect.GetComponent<ParticleSystem>().Play();
            
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
