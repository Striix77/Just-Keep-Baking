using System.Collections;
using UnityEngine;

public class Oven : Interactable
{
    public PlayerInventory playerInventory;
    public float bakeTime = 5f;
    private bool isBaking = false;
    public Transform SpawnPoint;

    protected override void Interact()
    {
        if (CanBake() && !isBaking)
        {
            RecipeSO currentRecipe = GameManager.Instance.GetCurrentRecipe();
            foreach (var ingredient in currentRecipe.ingredients)
            {
                playerInventory.RemoveItem(ingredient.item, ingredient.quantity);
            }
            StartCoroutine(BakeSelectedItem());
            isBaking = true;
        }
        else
        {
            Debug.Log("Cannot bake the selected recipe.");
        }
    }

    private bool CanBake()
    {
        RecipeSO currentRecipe = GameManager.Instance.GetCurrentRecipe();
        if (currentRecipe == null)
        {
            Debug.Log("No recipe selected.");
            return false;
        }
        foreach (var ingredient in currentRecipe.ingredients)
        {
            if (playerInventory.GetIngredientQuantity(ingredient.item) < ingredient.quantity)
            {
                Debug.Log("Not enough " + ingredient.item.itemName + " to bake " + currentRecipe.recipeName);
                return false;
            }
        }
        return true;
    }

    private IEnumerator BakeSelectedItem()
    {
        for (float t = 0; t < bakeTime; t += 1f)
        {
            promptMessage = "Baking " + GameManager.Instance.GetCurrentRecipe().recipeName + ": " + (bakeTime - t) + "s remaining";
            yield return new WaitForSeconds(1f);
        }
        isBaking = false;
        Instantiate(GameManager.Instance.GetCurrentRecipe().resultPrefab, SpawnPoint.position, Quaternion.identity);
        GameManager.Instance.SetGameState("Normal");
        Debug.Log("Finished baking " + GameManager.Instance.GetCurrentRecipe().recipeName);
        GameManager.Instance.SetCurrentRecipe(null);
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == "Baking")
        {
            RecipeSO currentRecipe = GameManager.Instance.GetCurrentRecipe();
            if (currentRecipe != null && !isBaking)
            {
                promptMessage = "Bake " + currentRecipe.recipeName;
            }
        }
        else
        {
            promptMessage = "No recipe selected.";
        }
    }
}
