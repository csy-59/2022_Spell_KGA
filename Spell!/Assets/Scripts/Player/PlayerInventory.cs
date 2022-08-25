using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private readonly int inventoryCapacity = 25;
    [SerializeField] private Transform InventoryPosition;
    private List<ItemInteract> inventory = new List<ItemInteract>();

    private PlayerInput input;
    private PlayerInteract interact;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        interact = GetComponent<PlayerInteract>();
    }

    public void SelectItem(int itemNumber)
    {
        if (itemNumber >= inventory.Count)
            return;

        ItemInteract selectedItem = inventory[itemNumber];
        inventory.RemoveAt(itemNumber);
        interact.PickItem(selectedItem);
    }

    public bool AddItemToInventory(ItemInteract item)
    {
        if(inventory.Count >= inventoryCapacity)
        {
            UIManager.Instance.SetInfoTextBar("Inventory is Full");
            return false;
        }

        if(item == null)
        {
            return false;
        }

        item.ToInventory(InventoryPosition);
        inventory.Add(item);

        return true;
    }

}
