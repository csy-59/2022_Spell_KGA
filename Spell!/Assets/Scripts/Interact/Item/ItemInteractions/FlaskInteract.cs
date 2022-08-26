using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;



public class FlaskInteract : ItemInteract
{
    [Serializable]
    class PotionRecipe
    {
        public ItemList[] RecipeItemlist;
        public EffectList effect;
        public Sprite effectSprite;
        public Color potionColor;
    }

    [Header ("Flask Ineraction")]
    private EffectList potionEffect;
    public ItemList changeItem;
    [SerializeField] private PotionRecipe[] potionRecipes;
    [SerializeField] private Color originalColor;
    private CauldronInteract[] cauldrons;

    [SerializeField] private Material flaskMaterial;
    private Sprite effectSprite;

    protected override void Awake()
    {
        base.Awake();

        potionEffect = EffectList.NoEffect;

        flaskMaterial.color = originalColor;
        cauldrons = FindObjectsOfType<CauldronInteract>();
    }

    public override bool Interact(ItemList item, ObjectList objectToInteract)
    {
        if(!base.Interact(item, objectToInteract))
        {
            return false;
        }

        CauldronInteract cauldron = cauldrons[0].ObjectType == objectToInteract ? cauldrons[0] : cauldrons[1];
        return SetEffect(cauldron.GetRecipe());
    }

    private bool SetEffect(List<ItemList> itemQueue)
    {

        for(int i = 0; i < potionRecipes.Length; ++i)
        {
            if(CheckRecipe(itemQueue, i))
            {
                potionEffect = potionRecipes[i].effect;
                flaskMaterial.color = potionRecipes[i].potionColor;
                effectSprite = potionRecipes[i].effectSprite;
                return true;
            }
        }

        potionEffect = EffectList.NoEffect;
        flaskMaterial.color = originalColor;
        effectSprite = null;

        return false;
    }

    private bool CheckRecipe(List<ItemList> itemQueue, int recipeNumber)
    {
        PotionRecipe recipe = potionRecipes[recipeNumber];

        if (itemQueue.Count != recipe.RecipeItemlist.Length)
        {
            return false;
        }

        for (int i = 0; i < itemQueue.Count; ++i)
        {
            if (itemQueue[i] != recipe.RecipeItemlist[i]
                && recipe.RecipeItemlist[i] != ItemList.DontCare)
            {
                return false;
            }
        }

        return true;
    }

    public override bool Use(PlayerInteraction player)
    {
        if(potionEffect != EffectList.NoEffect)
        {
            UIManager.Instance.SetInfoTextBar($"I fill something...!{potionEffect}");
            UIManager.Instance.SetEffectImage(effectSprite);
            player.SetPlayerEffect(potionEffect);
            potionEffect = EffectList.NoEffect;
            flaskMaterial.color = originalColor;
            return true;
        }

        UIManager.Instance.SetInfoTextBar("I fill nothing...");
        UIManager.Instance.SetEffectImage(effectSprite);
        player.SetPlayerEffect(potionEffect);

        return false;
    }
}
