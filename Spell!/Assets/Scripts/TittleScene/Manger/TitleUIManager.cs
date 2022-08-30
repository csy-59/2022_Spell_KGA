using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    [Header("Ending List")]
    [SerializeField] private GameObject endingScrollPanel;
    private Image endingScrollImage;
    public bool IsEndingScrollShown { get; private set; }

    [SerializeField] private Sprite[] endingScrollSprites;

    [Header("Information Text")]
    [SerializeField] private TextMeshProUGUI infomationText;

    [Header("Start Intro")]
    [SerializeField] private GameObject blackPanel;
    [SerializeField] private Sprite startScrollSprite;
    private Image blackPanelImage;
    private bool isStartScrollShown = false;

    private AudioSource ranningSound;

    private void Awake()
    {
        ranningSound = GetComponent<AudioSource>();

        blackPanelImage = blackPanel.GetComponent<Image>();
        blackPanel.SetActive(false);

        endingScrollImage = endingScrollPanel.GetComponent<Image>();
        endingScrollPanel.SetActive(false);
        IsEndingScrollShown = false;
    }

    private void Update()
    {

        if (isStartScrollShown && Input.anyKeyDown)
        {
            StartGame();
        }
        else if (IsEndingScrollShown && Input.GetKeyDown(KeyCode.Escape))
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

        IsEndingScrollShown = true;
        SetInformationText("Esc to Exit");
        endingScrollPanel.SetActive(IsEndingScrollShown);
    }

    public void CloseScroll()
    {
        IsEndingScrollShown = false;
        SetInformationText("");
        endingScrollPanel.SetActive(IsEndingScrollShown);
    }

    public void StartIntro()
    {
        ranningSound.Pause();
        StartCoroutine(ShowIntro());
    }

    private IEnumerator ShowIntro()
    {
        float currentAlpha = 0f;
        float endAlpha = 1f;

        blackPanel.SetActive(true);
        IsEndingScrollShown = true;

        while(true)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, endAlpha, 10 * Time.deltaTime);
            blackPanelImage.color = new Color(0f, 0f, 0f, currentAlpha);

            if(endAlpha - currentAlpha < 0.01f)
            {
                blackPanelImage.color = new Color(0f, 0f, 0f, 1f);
                break;
            }

            yield return null;
        }

        ShowScroll(startScrollSprite);
        SetInformationText("Press any key");
        isStartScrollShown = true;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}