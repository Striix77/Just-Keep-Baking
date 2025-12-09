using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Normal,
        Baking,
        Paused,
    }

    public static GameManager Instance;
    private RecipeSO currentRecipe;
    public GameState currentState = GameState.Normal;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetGameState(string newState)
    {
        currentState = (GameState)Enum.Parse(typeof(GameState), newState);
        Debug.Log("Game State changed to: " + currentState.ToString());
    }

    public string GetGameState()
    {
        return currentState.ToString();
    }

    public RecipeSO GetCurrentRecipe()
    {
        return currentRecipe;
    }

    public void SetCurrentRecipe(RecipeSO recipe)
    {
        currentRecipe = recipe;
    }
}
