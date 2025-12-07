using UnityEngine;

public class RecipeBookInteract : Interactable
{
    public GameObject recipeBookUI;
    private bool isOpen = false;

    public PlayerMovement playerMovement;

    public CameraController cameraController;

    protected override void Interact()
    {
        if (isOpen)
        {
            CloseRecipeBook();
        }
        else
        {
            OpenRecipeBook();
        }
    }

    private void OpenRecipeBook()
    {
        recipeBookUI.SetActive(true);
        isOpen = true;
        playerMovement.enabled = false;
        cameraController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseRecipeBook()
    {
        recipeBookUI.SetActive(false);
        isOpen = false;
        playerMovement.enabled = true;
        cameraController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}