using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : SingletonBehaviour<UIManager>
{
    public TextMeshProUGUI InfoText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetInfoTextBar(string info)
    {
        InfoText.text = info;
    }
}
