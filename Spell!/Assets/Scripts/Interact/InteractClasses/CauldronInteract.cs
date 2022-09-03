using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilityAsset;

public class CauldronInteract : InteractiveObject
{
    [Header("Cauldron Interaction")]
    [SerializeField] private Color[] effectColors;
    [SerializeField] private int capacity;
    private List<ItemList> magicMaterialsQueue = new List<ItemList>();

    [SerializeField] private Material waterMaterial;

    public WoodenPlankinteract fire;

    [SerializeField] private GameObject itemDropEffects;
    [SerializeField] private ParticleSystem[] itemDropEffectParticles;
    private AudioSource itemDropAudioSource;
    [SerializeField] private AudioClip itemDropAudioClip;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        waterMaterial.color = effectColors[0];
        audioSource = GetComponent<AudioSource>();

        itemDropAudioSource = itemDropEffects.GetComponent<AudioSource>();
        itemDropEffects.SetActive(false);
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        PlayerPrefsKey.SetMagicalMaterialList((int)MagicalMaterialList.Cauldron);

        if(!fire.IsFireOn)
        {
            UIManager.Instance.SetInfoTextBar("The water is cold... I can't make potion!");
            return false;
        }

        int interactionNumber = base.InteractPreAssert(item, effect);

        if (interactionNumber == -1)
        {
            if (item != ItemList.NoItem)
                UIManager.Instance.SetInfoTextBar("I don't think so...");
            else
                UIManager.Instance.SetInfoTextBar("I can make potion now!");
            return false;
        }

        if(interactionNumber == necessaryItem.Length - 1)
        {
            UIManager.Instance.SetInfoTextBar("I made something...!");
            return true;
        }

        if(magicMaterialsQueue.Count == capacity && 
            interactionNumber != 0)
        {
            UIManager.Instance.SetInfoTextBar("Cauldron is Full. It sould be reset");
            return false;
        }

        if(interactionNumber == 0)
        {
            UIManager.Instance.SetInfoTextBar("Water is Clear. Cauldron has been reset...");
            magicMaterialsQueue.Clear();
            waterMaterial.color = effectColors[0];
            PlayItemDropEffect();
            return true;
        }

        UIManager.Instance.SetInfoTextBar("Spell!");
        if(interactionNumber < effectColors.Length)
        {
            waterMaterial.color = effectColors[interactionNumber];
        }
        magicMaterialsQueue.Add(item);
        PlayItemDropEffect();

        return true;
    }

    private void PlayItemDropEffect()
    {
        itemDropEffects.SetActive(true);
        foreach (var particleSystem in itemDropEffectParticles)
        {
            var instantiatedParticalSystem = Instantiate(particleSystem, itemDropEffects.transform.position, 
                particleSystem.transform.rotation, itemDropEffects.transform);
            instantiatedParticalSystem.transform.localScale = gameObject.transform.localScale;

        }
        itemDropAudioSource.PlayOneShot(itemDropAudioClip);
    }

    public List<ItemList> GetRecipe()
    {
        return magicMaterialsQueue;
    }
}
