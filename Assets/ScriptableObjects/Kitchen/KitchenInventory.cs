using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Inventory", menuName = "Inventory/Kitchen Inventory")]
public class KitchenInventory : ScriptableObject
{
    [System.Serializable]
    public struct IngredientSlot
    {
        public ItemSO item;
        public int quantity;
    }

    public List<IngredientSlot> ingredients = new List<IngredientSlot>();

    public void AddIngredient(ItemSO item, int quantity)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].item == item)
            {
                IngredientSlot slot = ingredients[i];
                slot.quantity += quantity;
                ingredients[i] = slot;
                return;
            }
        }

        IngredientSlot newSlot = new IngredientSlot { item = item, quantity = quantity };
        ingredients.Add(newSlot);
    }

    public bool RemoveIngredient(ItemSO item, int quantity)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].item == item)
            {
                if (ingredients[i].quantity >= quantity)
                {
                    IngredientSlot slot = ingredients[i];
                    slot.quantity -= quantity;
                    ingredients[i] = slot;

                    if (ingredients[i].quantity == 0)
                    {
                        ingredients.RemoveAt(i);
                    }

                    return true;
                }
                else
                {
                    return false; // Not enough quantity
                }
            }
        }

        return false; // Item not found
    }

    public int GetIngredientQuantity(ItemSO item)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].item == item)
            {
                return ingredients[i].quantity;
            }
        }

        return 0; // Item not found
    }

}
