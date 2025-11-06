using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
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

    public bool HasItem(ItemSO item, int amount)
    {
        int index = items.FindIndex(x => x.item == item);
        return index != -1 && items[index].quantity >= amount;
    }


}
