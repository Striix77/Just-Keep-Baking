using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    public RecipeSO recipeData;
    public KitchenInventory kitchenInventory;

    public PlayerInventory playerInventory;

    [SerializeField] private TMP_Text recipeLabel;
    [SerializeField] private string missingSuffix = " (Missing Ingredients)";
    [SerializeField] private Color availableColor = new Color(149f / 255f, 138f / 255f, 109f / 255f);
    [SerializeField] private Color missingColor = new Color(80f / 255f, 70f / 255f, 60f / 255f);
    private Button recipeButton;
    public GameObject RequirementsPanel;
    public TextMeshProUGUI RequirementsText;
    public RecipeBookInteract recipeBookInteract;

    void Awake()
    {
        recipeButton = GetComponent<Button>();
    }

    void OnEnable()
    {
        RefreshAvailability();
    }

    public void RefreshAvailability()
    {
        if (recipeData == null || kitchenInventory == null)
        {
            return;
        }

        bool hasAllIngredients = HasAllIngredients();

        if (recipeLabel != null)
        {
            recipeLabel.text = hasAllIngredients
                ? recipeData.recipeName
                : recipeData.recipeName + missingSuffix;
        }

        if (recipeButton != null)
        {
            recipeButton.interactable = hasAllIngredients;
            ColorBlock colors = recipeButton.colors;
            colors.normalColor = hasAllIngredients ? availableColor : missingColor;
            recipeButton.colors = colors;

        }


    }

    private bool HasAllIngredients()
    {
        foreach (var ingredient in recipeData.ingredients)
        {
            if (kitchenInventory.GetIngredientQuantity(ingredient.item) + playerInventory.GetIngredientQuantity(ingredient.item) < ingredient.quantity)
            {
                Debug.Log("Missing ingredient: " + ingredient.item.itemName + " Required: " + ingredient.quantity + " Available: " + kitchenInventory.GetIngredientQuantity(ingredient.item));
                return false;
            }
        }

        return true;
    }

    public void OnRecipeSelected()
    {
        if (HasAllIngredients())
        {
            RequirementsPanel.SetActive(true);
            RequirementsText.text = "";
            foreach (var ingredient in recipeData.ingredients)
            {
                RequirementsText.text += $"{ingredient.item.itemName}: {ingredient.quantity}\n";
            }
            recipeBookInteract.CloseRecipeBook();
            GameManager.Instance.SetGameState("Baking");
            GameManager.Instance.SetCurrentRecipe(recipeData);
        }
        else
        {
            Debug.Log("Cannot bake " + recipeData.recipeName + ". Missing ingredients.");
        }

        RefreshAvailability();
    }

}
