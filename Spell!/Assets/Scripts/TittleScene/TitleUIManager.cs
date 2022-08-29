using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [Header("Ending List")]
    [SerializeField] private GameObject endingScrollPanel;
    private Image endingScrollImage;
    public bool IsEndingScroll { get; private set; }

    [SerializeField] private Sprite[] endingScrollSprites;

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
            CloseEndingScroll();
        }
    }

    public void ShowEndingScroll(int endingNumber)
    {
        endingScrollImage.sprite = endingScrollSprites[endingNumber];

        IsEndingScroll = true;
        endingScrollPanel.SetActive(IsEndingScroll);
    }

    public void CloseEndingScroll()
    {
        IsEndingScroll = false;
        endingScrollPanel.SetActive(IsEndingScroll);
    }

}