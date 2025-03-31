using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour {
    public static GameInputManager Instance { get; private set; }

    private PlayerInputActions inputActions;

    private bool interactPressed = false;

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
            EventManager.Instance.playerInputEvents.Jump();
            interactPressed = true;
        };
        inputActions.Player.Jump.canceled += ctx => {
            EventManager.Instance.playerInputEvents.JumpCanceled();
            interactPressed = false;
        };

        // Interact
        inputActions.Player.Interact.performed += ctx => EventManager.Instance.playerInputEvents.Interact();

        // Continue
        inputActions.Player.Continue.performed += ctx => EventManager.Instance.playerInputEvents.Continue();
    }

    private void OnDisable() {
        inputActions.Disable();

        // Jump
        inputActions.Player.Jump.performed -= ctx => EventManager.Instance.playerInputEvents.Jump();
        inputActions.Player.Jump.canceled -= ctx => EventManager.Instance.playerInputEvents.JumpCanceled();

        // Interact
        inputActions.Player.Interact.performed -= ctx => EventManager.Instance.playerInputEvents.Interact();

        // Continue
        inputActions.Player.Continue.performed -= ctx => EventManager.Instance.playerInputEvents.Continue();

    }

    public Vector2 GetMovementVectorNormalize() {
        return inputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }

    public bool GetSubmitPress() {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }
}
