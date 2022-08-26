using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronInteract : InteractiveObject
{
    [Header("Cauldron Interaction")]
    [SerializeField] private Color[] effectColors;
    [SerializeField] private int capacity;
    private List<ItemList> magicMaterialsQueue = new List<ItemList>();

    [SerializeField] private Material waterMaterial;

    public WoodenPlankinteract fire;

    protected override void Awake()
    {
        base.Awake();

        waterMaterial.color = effectColors[0];
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(!fire.IsFireOn)
        {
            UIManager.Instance.SetInfoTextBar("The wate is cold... I can't make potion!");
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
            return true;
        }

        UIManager.Instance.SetInfoTextBar("Spell!");
        waterMaterial.color = effectColors[interactionNumber];
        magicMaterialsQueue.Add(item);
        return true;
    }

    public List<ItemList> GetRecipe()
    {
        return magicMaterialsQueue;
    }
}
