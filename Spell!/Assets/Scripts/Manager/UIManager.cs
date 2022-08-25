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
    private readonly Color readyColor = new Color(1f, 1f, 1f, 0f);

    delegate void MyButtonEvent(int i);

    private void Awake()
    {
        SetInfoTextBar("");

        for(int i = 0; i< inventoryCapacity; ++i)
        {
            Button newButton = Instantiate(ButtonPrefab, ItemPanel.transform);
            newButton.transform.localPosition = new Vector2(
                xStartPos + positionOffset * (i % 10),
                yStartPos - positionOffset * (i / 10));

            int temp = i;
            newButton.onClick.AddListener(() => { 
                inventoryScript.SelectItem(temp); 
            });

            Image itemImage = newButton.GetComponentInChildren<Image>();
            itemImage.sprite = null;
            itemImage.color = readyColor;

            itemInventory.Add(itemImage);
        }

        inventoryPanel.SetActive(false);
    }

    public void SetInfoTextBar(string info)
    {
        InfoText.text = info;
    }

    public void ShowInventory(List<ItemInteract> inventory)
    {
        isInventoryShown = !isInventoryShown;
        IsUIShown = isInventoryShown;

        inventoryPanel.SetActive(isInventoryShown);

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
            itemInventory[i].color = originalColor;
        }
    }
}
