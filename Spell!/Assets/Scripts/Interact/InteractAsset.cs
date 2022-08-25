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

        Scroll,

        BlackShard,
        BlueShard,
        RedShard,
        PinkShard,
        PurpleShard,
        YellowShard,

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
        EffectMax
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

        ObjectMax
    }

    public static class CurrentItem
    {

    }
}
