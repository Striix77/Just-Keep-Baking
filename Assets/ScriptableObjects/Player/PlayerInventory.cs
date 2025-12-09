using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    [System.Serializable]
    public struct PlayerSlot
    {
        public ItemSO item;
        public int quantity;
    }

    public List<PlayerSlot> items = new List<PlayerSlot>();
    public int maxSlots = 10;

    public bool AddItem(ItemSO newItem, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == newItem)
            {
                PlayerSlot updatedSlot = items[i];
                updatedSlot.quantity += quantity;
                items[i] = updatedSlot;
                return true;
            }
        }

        if (items.Count < maxSlots)
        {
            PlayerSlot newSlot = new PlayerSlot { item = newItem, quantity = quantity };
            items.Add(newSlot);
            return true;
        }

        Debug.Log("Inventory Full");
        return false;
    }

    public bool RemoveItem(ItemSO itemToRemove, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == itemToRemove)
            {
                if (items[i].quantity >= quantity)
                {
                    PlayerSlot updatedSlot = items[i];
                    updatedSlot.quantity -= quantity;

                    if (updatedSlot.quantity <= 0)
                    {
                        items.RemoveAt(i);
                    }
                    else
                    {
                        items[i] = updatedSlot;
                    }
                    return true;
                }
                else
                {
                    Debug.Log("Not enough quantity to remove");
                    return false;
                }
            }
        }

        Debug.Log("Item not found in inventory");
        return false;
    }

    public int GetIngredientQuantity(ItemSO item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                return items[i].quantity;
            }
        }

        return 0; // Item not found
    }
}
