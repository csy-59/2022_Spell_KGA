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

    [Header("Effect Ending")]
    [SerializeField] private EndingList[] endingType;

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

    private bool SetEffect(List<ItemList> itemList)
    {

        for(int i = 0; i < potionRecipes.Length; ++i)
        {
            if(CheckRecipe(itemList, i))
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

    private bool CheckRecipe(List<ItemList> itemList, int recipeNumber)
    {
        PotionRecipe recipe = potionRecipes[recipeNumber];

        if (itemList.Count != recipe.RecipeItemlist.Length)
        {
            return false;
        }

        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i] != recipe.RecipeItemlist[i])
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
            effectSprite = null;
            flaskMaterial.color = originalColor;

            if ((int)potionEffect >= (int) EffectList.ChangeToSkeleton)
            {
                GameManager.Instance.SetEnding(endingType[(int)potionEffect - (int)EffectList.ChangeToSkeleton]);
                return false;
            }
            
            return true;
        }
        else
        {
            UIManager.Instance.SetInfoTextBar("I fill nothing...");
            UIManager.Instance.SetEffectImage(effectSprite);
            player.SetPlayerEffect(potionEffect);
        }

        return false;
    }
}
