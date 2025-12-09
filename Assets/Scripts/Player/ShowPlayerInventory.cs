using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowPlayerInventory : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI inventoryItemsDisplay;

    void Update()
    {
        if (inventoryItemsDisplay != null)
        {
            inventoryItemsDisplay.text = "";
            foreach (var slot in playerInventory.items)
            {
                inventoryItemsDisplay.text += $"{slot.item.itemName} x{slot.quantity}{slot.item.unitOfMeasure}\n";
            }
        }
    }

}
