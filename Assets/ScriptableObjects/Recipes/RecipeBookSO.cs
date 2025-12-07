using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Book", menuName = "Inventory/RecipeBook")]
public class RecipeBookSO : ScriptableObject
{
    public RecipeSO[] recipes;
}

