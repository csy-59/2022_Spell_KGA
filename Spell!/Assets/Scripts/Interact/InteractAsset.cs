using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractAsset
{
    public enum ItemList
    {

        ItemMax
    }

    public interface IInteractive
    {
        void Interact();
    }

    public static class CurrentItem
    {

    }
}
