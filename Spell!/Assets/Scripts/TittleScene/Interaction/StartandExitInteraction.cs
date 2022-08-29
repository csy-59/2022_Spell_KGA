using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartandExitInteraction : InteractiveObject
{
    [Header("Start Or Exit")]
    [SerializeField] private bool isStart = true;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnFocus()
    {
        outline.enabled = true;
    }

    public override void OutFocus()
    {
        outline.enabled = false;
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(isStart)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("quit");
            Application.Quit();
        }

        return false;
    }
}
