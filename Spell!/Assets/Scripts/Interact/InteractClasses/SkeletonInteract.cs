using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInteract : InteractiveObject
{
    private AudioSource audioSource;
    [SerializeField] AudioClip boneAudioClip;

    [Header ("\nSkeleton Item Interaction")]
    [SerializeField] private GameObject item;
    [SerializeField] private string getItemLine = "SomeThing dropped";
    [SerializeField] private string afterItemLine = "I think it's better not to touch him...";
    private bool isItemGiven = false;

    [Header ("\nSkeleton bone Interaction")]
    [SerializeField] private GameObject bone;
    [SerializeField] private Transform bonePosition;
    [SerializeField] private string getBoneLine = "He's already dead, right?";
    private bool isBoneGiven = false;

    protected override void Awake()
    {
        base.Awake();

        necessaryEffect = new EffectList[]
        {
            EffectList.DontCare,
            EffectList.Power
        };
        necessaryItem = new ItemList[]
        {
            ItemList.DontCare,
            ItemList.DontCare
        };

        item.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        audioSource.PlayOneShot(boneAudioClip);

        if(base.InteractPreAssert(item, effect, 1))
        {
            if(!isBoneGiven)
            {
                UIManager.Instance.SetInfoTextBar(getBoneLine);
                GameObject newBone = Instantiate(bone, bonePosition.position, bonePosition.rotation);
                newBone.name = bone.name;
                isBoneGiven = true;
                return true;
            }
        }

        if (isItemGiven)
        {
            UIManager.Instance.SetInfoTextBar(afterItemLine);
            return false;
        }

        UIManager.Instance.SetInfoTextBar(getItemLine);
        this.item.SetActive(true);
        isItemGiven = true;
        return true;
    }
}
