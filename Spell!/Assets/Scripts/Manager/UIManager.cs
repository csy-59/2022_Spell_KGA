using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonBehaviour<UIManager>
{
    public TextMeshProUGUI InfoText;
    public TextMeshProUGUI InstructionText;

    [Header("Effect")]
    [SerializeField] private Image effectImage;

    [Header("Inventory")]
    public int inventoryCapacity;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject ItemPanel;
    public PlayerInventory inventoryScript;
    
    public Button ButtonPrefab;
    [SerializeField] private float xStartPos = 40f;
    [SerializeField] private float yStartPos = -40f;
    [SerializeField] private float positionOffset = 90f;
    
    private List<Image> itemInventory = new List<Image>();

    public bool IsUIShown = false;
    private bool isInventoryShown = false;

    private readonly Color originalColor = new Color(1f, 1f, 1f, 1f);
    private readonly Color originalColorBlack = new Color(0f, 0f, 0f, 1f);
    private readonly Color readyColor = new Color(1f, 1f, 1f, 0f);
    private readonly Color readyColorBlack = new Color(0f, 0f, 0f, 0f);

    [Header("Black Out")]
    [SerializeField] private GameObject blackOutPanel;
    private Image blackOutImage;
    private bool isBlackOutPanelShown = false;

    [Header("Scroll")]
    [SerializeField] private GameObject scrollPanel;
    private Image scrollImage;
    private bool isScrollShown = false;

    private void Awake()
    {
        SetInfoTextBar("");

        GridLayoutGroup itemPanelGrideGroup = ItemPanel.GetComponent<GridLayoutGroup>();
        RectTransform itemPanelRectTransform = ItemPanel.GetComponent<RectTransform>();

        itemPanelGrideGroup.cellSize = new Vector2(
            itemPanelRectTransform.rect.width / 10,
            itemPanelRectTransform.rect.width / 10
            );

        for(int i = 0; i< inventoryCapacity; ++i)
        {
            Button newButton = Instantiate(ButtonPrefab, ItemPanel.transform);

            int temp = i;
            newButton.onClick.AddListener(() => { 
                inventoryScript.SelectItem(temp); 
            });

            Image itemImage = newButton.GetComponentsInChildren<Image>()[2];
            itemImage.sprite = null;
            itemImage.color = readyColor;

            itemInventory.Add(itemImage);
        }

        effectImage.sprite = null;

        inventoryPanel.SetActive(false);
        
        blackOutImage = blackOutPanel.GetComponent<Image>();
        blackOutImage.color = readyColorBlack;
        blackOutPanel.SetActive(false);

        scrollImage = scrollPanel.GetComponent<Image>();
        scrollPanel.SetActive(false);
    }

    public void SetInfoTextBar(string info)
    {
        InfoText.text = info;
    }

    public void ShowInventory(List<ItemInteract> inventory)
    {
        if (IsUIShown && !isInventoryShown)
            return;

        isInventoryShown = !isInventoryShown;
        inventoryPanel.SetActive(isInventoryShown);
        SetIsUIShown();

        if(isInventoryShown)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            SetInventory(inventory);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetInventory(List<ItemInteract> inventory)
    {
        for(int i = 0; i < inventory.Count; ++i)
        {
            itemInventory[i].sprite = inventory[i].itemImage;
            itemInventory[i].color = originalColor;
        }

        for(int i = inventory.Count; i < inventoryCapacity; ++i)
        {
            itemInventory[i].color = readyColor;
        }
    }

    public delegate void BlackOutEvent();

    public void BlackOut(float speed, BlackOutEvent beforeBlackOut, 
        BlackOutEvent afterBlackOut)
    {
        isBlackOutPanelShown = true;
        blackOutPanel.SetActive(true);
        SetIsUIShown();

        beforeBlackOut.Invoke();
        StartCoroutine(BlackOut(0f, 1f, speed, afterBlackOut));
    }

    private IEnumerator BlackOut(float startAlpha, float endAlpha, float speed, 
        BlackOutEvent afterBlackOut)
    {
        float currentAlpha = startAlpha;

        while(true)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, endAlpha, speed * Time.deltaTime);
            
            blackOutImage.color = new Color(0f, 0f, 0f, currentAlpha);
            if(Mathf.Abs(endAlpha - currentAlpha) < 0.01f)
            {
                blackOutImage.color = originalColorBlack;
                break;
            }

            yield return null;
        }

        afterBlackOut?.Invoke();

        currentAlpha = endAlpha;

        while (true)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, startAlpha, speed * Time.deltaTime);

            blackOutImage.color = new Color(0f, 0f, 0f, currentAlpha);
            if (Mathf.Abs(startAlpha - currentAlpha) < 0.01f)
            {
                blackOutImage.color = readyColor;
                break;
            }

            yield return null;
        }

        isBlackOutPanelShown = false;
        blackOutPanel.SetActive(false);
        SetIsUIShown();
    }

    public void ShowScroll(Sprite scrollSprite)
    {
        if (IsUIShown && !isScrollShown)
            return;

        isScrollShown = !isScrollShown;
        scrollPanel.SetActive(isScrollShown);
        SetIsUIShown();

        scrollImage.sprite = scrollSprite;
    }

    private void SetIsUIShown()
    {
        IsUIShown = isInventoryShown | isBlackOutPanelShown | isScrollShown;
    }

    public void SetEffectImage(Sprite effectSprite)
    {
        if (effectSprite)
        {
            effectImage.color = originalColor;
            effectImage.sprite = effectSprite;
        }
        else
        {
            effectImage.color = readyColor;
            effectImage.sprite = null;
        }
    }
}
