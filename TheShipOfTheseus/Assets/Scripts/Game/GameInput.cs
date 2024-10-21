using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInspectAction;

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Inspect.performed += Inspect_performed;
    }

    private void OnDestroy() 
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Inspect.performed -= Inspect_performed;

        playerInputActions.Dispose();
    }

    private void Inspect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInspectAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

}
