using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory/Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite recipeIcon;
    public string description;


    [System.Serializable]
    public struct Ingredient
    {
        public ItemSO item;
        public int quantity;
    }

    public Ingredient[] ingredients;

}

