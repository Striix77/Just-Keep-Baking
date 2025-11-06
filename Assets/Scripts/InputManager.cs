using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public static PlayerInput.PlayerActions playerActions;
    private static bool holding;

    void Awake()
    {
        playerInput = new PlayerInput();
        playerActions = playerInput.Player;
        playerActions.Hold.performed += HoldPerformed;
        playerActions.Hold.canceled += HoldCanceled;
    }

    private void HoldCanceled(InputAction.CallbackContext context)
    {
        holding = false;
    }

    private void HoldPerformed(InputAction.CallbackContext context)
    {
        holding = true;
    }

    public static bool IsHolding()
    {
        return holding;
    }

    void Update()
    {
    }

    void OnEnable()
    {
        playerActions.Enable();
    }
    void OnDisable()
    {
        playerActions.Disable();
    }

}
