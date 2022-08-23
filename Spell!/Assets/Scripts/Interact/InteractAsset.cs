using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractAsset
{
    public enum ItemList
    {
        NoItem,
        Tourch,
        TourchWithFire,
        Cheese,
        Meat,
        DontCare,
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
