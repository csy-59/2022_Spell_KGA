using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : SingletonBehaviour<UIManager>
{
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI InstructionText;

    private void Awake()
    {
        SetInfoTextBar("");
    }

    void Update()
    {
        
    }

    public void SetInfoTextBar(string info)
    {
        InfoText.text = info;
    }

    public void SetInstructionText(string instruction)
    {
        InstructionText.text = instruction;
    }
}
