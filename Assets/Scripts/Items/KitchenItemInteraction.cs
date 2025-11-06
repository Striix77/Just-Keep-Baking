using UnityEngine;

public class KitchenItemInteraction : Interactable
{
    public ItemSO kitchenItem;
    public int quantity = 1;
    public KitchenInventory kitchenInventory;
    public PlayerInventory playerInventory;

    protected override void Interact()
    {
        if (kitchenInventory.GetIngredientQuantity(kitchenItem) >= quantity)
        {
            bool removed = kitchenInventory.RemoveIngredient(kitchenItem, quantity);
            if (removed)
            {
                bool added = playerInventory.AddItem(kitchenItem, quantity);
                if (added)
                {
                    Debug.Log("Transferred " + quantity + " " + kitchenItem.itemName + "(s) to Player Inventory.");
                }
                else
                {
                    // If adding to player inventory failed, return item to kitchen inventory
                    kitchenInventory.AddIngredient(kitchenItem, quantity);
                    Debug.Log("Player Inventory Full. Could not transfer item.");
                }
            }
        }
        else
        {
            Debug.Log("Not enough " + kitchenItem.itemName + " in Kitchen Inventory.");
        }
    }
}
