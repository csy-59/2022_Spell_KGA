using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    private int capacity;
    [SerializeField] private Transform position;
    private List<ItemInteract> inventory = new List<ItemInteract>();

    private PlayerInput input;
    private PlayerInteraction interaction;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        interaction = GetComponent<PlayerInteraction>();
        capacity = UIManager.Instance.inventoryCapacity;
    }

    private void Update()
    {
        if(input.E)
        {
            UIManager.Instance.ShowInventory(inventory);
        }
    }

    public void SelectItem(int itemNumber)
    {
        if (itemNumber >= inventory.Count)
            return;

        ItemInteract selectedItem = inventory[itemNumber];

        ItemInteract previousItem;
        interaction.SelectItem(selectedItem, out previousItem);
        previousItem.ToInventory(position);
        inventory[itemNumber] = previousItem;

        UIManager.Instance.SetInventory(inventory);
    }

    public bool AddItemToInventory(ItemInteract item)
    {
        if(inventory.Count >= capacity)
        {
            UIManager.Instance.SetInfoTextBar("Inventory is Full");
            return false;
        }

        if(item == null)
        {
            return false;
        }

        item.ToInventory(position);
        inventory.Add(item);

        return true;
    }

}
