using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollInteract : ItemInteract
{
    [SerializeField] private Sprite scrollSprite;

    protected override void Awake()
    {
        base.Awake();
    }

    public override bool Use(PlayerInteraction player)
    {
        UIManager.Instance.ShowScroll(scrollSprite);
        return true;
    }
}
