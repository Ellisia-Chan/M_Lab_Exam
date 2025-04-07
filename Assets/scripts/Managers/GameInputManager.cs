using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {
    public static GameInputManager Instance { get; private set; }

    private PlayerInputActions inputActions;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("GameInputManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

        inputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        inputActions.Enable();

        // Jump
        inputActions.Player.Jump.performed += ctx => {
            EventBus.Publish(new PlayerInputEventJumpAction());
        };
        inputActions.Player.Jump.canceled += ctx => {
            EventBus.Publish(new PlayerInputEventJumpCancelled());
        };

        // Interact
        inputActions.Player.Interact.performed += ctx => EventBus.Publish(new PlayerInputEventInteract());

        // Continue
        inputActions.Player.Continue.performed += ctx => EventBus.Publish(new PlayerInputEventContinue());
    }

    private void OnDisable() {
        inputActions.Disable();

        // Jump
        inputActions.Player.Jump.performed -= ctx => EventBus.Publish(new PlayerInputEventJumpAction());
        inputActions.Player.Jump.canceled -= ctx => EventBus.Publish(new PlayerInputEventJumpCancelled());

        // Interact
        inputActions.Player.Interact.performed -= ctx => EventBus.Publish(new PlayerInputEventInteract());

        // Continue
        inputActions.Player.Continue.performed -= ctx => EventBus.Publish(new PlayerInputEventContinue());

    }


    /// <summary>
    /// Returns the normalized movement vector from the player's input actions.
    /// </summary>
    /// <returns>A Vector2 representing the normalized movement direction.</returns>
    public Vector2 GetMovementVectorNormalize() {
        return inputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }

    public void DisableInputs() => inputActions.Disable();
    public void EnableInputs() => inputActions.Enable();
}
