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

    public interface IInteractive
    {
        void Interact();
    }

    public static class CurrentItem
    {

    }
}
