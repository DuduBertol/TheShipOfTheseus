using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInspectAction;
    public event EventHandler OnRotationAction;
    public event EventHandler OnRotationCanceledAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnResetRotationAction;

    private PlayerInputActions playerInputActions;

    private void Awake() 
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Inspect.performed += Inspect_performed;
        playerInputActions.Player.Rotation.performed += Rotation_performed;
        playerInputActions.Player.Rotation.canceled += Rotation_canceled;
        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.ResetRotation.performed += ResetRotation_performed;
    }

    private void OnDestroy() 
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Inspect.performed -= Inspect_performed;
        playerInputActions.Player.Rotation.performed -= Rotation_performed;
        playerInputActions.Player.Rotation.canceled -= Rotation_canceled;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Player.ResetRotation.performed -= ResetRotation_performed;

        playerInputActions.Dispose();
    }

    public Vector2 GetMouseMovementVectorNormalizedPlayer()
    {
        Vector2 mouseVector = playerInputActions.Player.Mouse.ReadValue<Vector2>();

        // mouseVector = mouseVector.normalized;

        return mouseVector;
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    private void ResetRotation_performed(InputAction.CallbackContext context)
    {
        OnResetRotationAction?.Invoke(this, EventArgs.Empty);
    }

    private void Rotation_canceled(InputAction.CallbackContext context)
    {
        OnRotationCanceledAction?.Invoke(this, EventArgs.Empty);
    }

    private void Rotation_performed(InputAction.CallbackContext context)
    {
        OnRotationAction?.Invoke(this, EventArgs.Empty);
    }

    private void Inspect_performed(InputAction.CallbackContext obj)
    {
        OnInspectAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

}
