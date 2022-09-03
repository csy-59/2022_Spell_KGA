using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class BirdInteract : InteractiveObject
{
    [Header("Bird Interact")]
    [SerializeField] private AudioClip guguAudioClip;
    [SerializeField] private GameObject hintScroll;
    private bool isHintGiven = false;

    [Header ("Feather Interact")]
    [SerializeField] private GameObject feather;
    [SerializeField] private Transform featherPosition;
    [SerializeField] private int featherPickedCount;

    [SerializeField] private Transform pigeonModel;
    [SerializeField] private Animator pigeonAnim;
    [SerializeField] private Transform awayPoint;
    [SerializeField] private float flySpeed;
    [SerializeField] private AudioClip flyAwayAudioClip;
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        hintScroll.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if(!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        audioSource.PlayOneShot(guguAudioClip);

        if(base.InteractPreAssert(item, effect) == -1)
        {
            if(isHintGiven)
            {
                UIManager.Instance.SetInfoTextBar("Bird seems happy");
                return false;
            }
            else
            {
                UIManager.Instance.SetInfoTextBar("The Bird is staring at me. He seems hungry. He need");
                return false;
            }
        }

        if(base.InteractPreAssert(item, effect, 0))
        {
            UIManager.Instance.SetInfoTextBar("The Bird is eating meat.");
            hintScroll.SetActive(true);
            isHintGiven = true;
            return true;
        }

        if(base.InteractPreAssert(item, effect, 1))
        {
            if(featherPickedCount > 0)
            {
                UIManager.Instance.SetInfoTextBar("The Bird is annoyed because of the lost feather");
                pigeonAnim.SetTrigger(AnimationID.Bird_Pick);
                --featherPickedCount;
                Instantiate(feather, featherPosition);
                return true;
            }

            UIManager.Instance.SetInfoTextBar("The Bird left");
            FlyAway();
            return true;
        }

        return false;
    }

    private void FlyAway()
    {
        pigeonModel.LookAt(awayPoint);
        pigeonAnim.SetBool(AnimationID.Bird_Fly, true);

        PlayerPrefsKey.SetCommonItemList((int)CommonItemList.Pigeon);

        ObjectMove.Instance.ObjectMoveToTargetPosition(
            pigeonModel, awayPoint.position, flySpeed,
            new ObjectMove.BeforeService(() => { Debug.Log("gogo"); gameObject.layer = LayerMask.NameToLayer("Default"); } ),
            new ObjectMove.AfterService(() => { Debug.Log("byebye"); Destroy(gameObject); })
            );

        audioSource.PlayOneShot(flyAwayAudioClip);
    }
}
