using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [Header("Ending List")]
    [SerializeField] private GameObject endingScrollPanel;
    private Image endingScrollImage;
    public bool IsEndingScroll { get; private set; }

    [SerializeField] private Sprite[] endingScrollSprites;

    [SerializeField] private TextMeshProUGUI infomationText;

    private void Awake()
    {
        endingScrollImage = endingScrollPanel.GetComponent<Image>();
        endingScrollPanel.SetActive(false);
        IsEndingScroll = false;
    }

    private void Update()
    {
        if(IsEndingScroll && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseScroll();
        }
    }

    public void ShowEndingScroll(int endingNumber)
    {
        ShowScroll(endingScrollSprites[endingNumber]);
    }

    public void SetInformationText(string text)
    {
        infomationText.text = text;
    }

    public void ShowScroll(Sprite scrollSprite)
    {
        endingScrollImage.sprite = scrollSprite;

        IsEndingScroll = true;
        endingScrollPanel.SetActive(IsEndingScroll);
    }

    public void CloseScroll()
    {
        IsEndingScroll = false;
        endingScrollPanel.SetActive(IsEndingScroll);
    }
}