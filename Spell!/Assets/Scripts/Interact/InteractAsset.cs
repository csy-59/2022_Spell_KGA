using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractAsset
{
    public enum ItemList
    {
        NoItem,
        DontCare,
        
        Tourch,
        TourchWithFire,
        Cheese,
        Meat,
        Key,
        FireStone,
        Feather,
        Bone,
        WoodenBoard,
        Cloth,
        Moss,
        Spoon,
        Bowl,

        Scroll,

        BlackShard,
        BlueShard,
        RedShard,
        PinkShard,
        PurpleShard,
        YellowShard,
        WhiteShard,

        Flask,

        RatHair,

        ItemMax
    }

    public enum EffectList
    {
        NoEffect,
        
        Power,
        Speed,
        Goo,
        Stretch,

        FireBall,
        Sticky,
        SuperPower,
        AntMan,
        Telaport,
        Change,

        DontCare,

        ChangeToBird,
        ChangeToSkeleton,
        ChangeToSlime,
        ChangeToRat,

        EffectMax
    }

    public enum ChangeEndingList
    {
        Bird,
        Skeleton,
        Slime,
        Rat
    }

    public enum ObjectList
    {
        NoObject,
        DontCare,

        Wall,
        Tourch,
        Window,
        Bird,
        RatHole,
        Bed,
        Skeleton,
        Door,
        Cauldron,
        Item,
        Chest,
        Cupboard,
        Cristal,
        Firepit,
        Switch,
        Clock,
        WoodenPlank,
        SmallCauldron,
        BigCauldron,
        WoodenPlankWithFire,
        FirepitWithFire,

        ObjectMax
    }
    
    public enum EndingList
    {
        SecretPath,
        Sticky,
        Break,
        Teleport,
        Shurink,
        ChangeToBird,
        ChangeToSkeleton,
        ChangeToSlime,
        ChangeToRat,
        
        NoEnding,
        EndingMax
    }

    public enum ScrollList
    {
        FirstScroll,
        PotionMaking101,
        CristalEffectScroll,
        SecretPathScroll,
        WizardScroll,
        PieonScroll,
        SecretChestScroll,
        FireBallScroll,
        SuperPowerScroll,
        StickyScroll,
        ShurinkScroll,
        TeleportScroll,
        ChangePotionScroll,
        ScrollMax
    }

    public enum CristalList
    {
        WhiteScristal,
        PurpleCristal,
        BlueCristal,
        RedCristal,
        BlackCristal,
        YellowCristal,
        PinkCristal,
        CristalMax
    }

    public enum MagicalMaterialList
    {
        Moss,
        Cauldron,
        Flask,
        FireStone,
        Bone,
        Feather,
        RatFur,
        MagicalMaterialList
    }

    public enum CommonItemList
    {
        Spoon,
        Bowl,
        Torch,
        Pigeon,
        Cheese,
        Meat,
        Key,
        Chest,
        CommonItemMax
    }
}
