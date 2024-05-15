using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public List<ItemClass> items = new List<ItemClass>();
    public Text inventoryText;

    public void AddItem(ItemClass item)
    {
        ItemClass existingItem = FindItemByName(item.itemName);
        if (existingItem != null)
        {
            existingItem.quantity += item.quantity;
        }
        else
        {
            items.Add(item);
            item.quantity = 1;
        }
        UpdateInventoryText();
    }

    public void RemoveItem(ItemClass item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            UpdateInventoryText();
        }
    }

    public void ChangeItemQuantity(string itemName, int amount){
        ItemClass existingItem = FindItemByName(itemName);
        if (existingItem != null)
        {
            existingItem.quantity += amount;

            if (existingItem.quantity <= 0){
                RemoveItem(existingItem);
            }else{
                UpdateInventoryText();
            }
        }
    }

    public ItemClass FindItemByName(string itemName)
    {
        foreach (ItemClass item in items)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public int GetCount (string itemName){
        ItemClass item = FindItemByName(itemName);
        if (item != null){
            return item.quantity;
        }else{
            return 0;
        }
    }

    public void UseItem(ItemClass item)
    {
        Debug.Log("Using item: " + item.itemName);
    }

    private void UpdateInventoryText()
    {
        if (inventoryText != null)
        {
            inventoryText.text = "Inventory:\n";
            foreach (ItemClass item in items)
            {
                inventoryText.text += item.itemName + ": " + item.quantity + "\n";
            }
        }
    }
}
