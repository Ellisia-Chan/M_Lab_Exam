using System;

public class PlayerInputEvents {
    public event Action OnJumpAction;
    public event Action OnJumpActionCanceled;

    public event Action OnInteractAction;

    // Jump
    public void Jump() => OnJumpAction?.Invoke();
    public void JumpCanceled() => OnJumpActionCanceled?.Invoke();

    //Interact
    public void Interact() => OnInteractAction?.Invoke();

    public void Clear() {
        if (EventManager.Instance != null) {
            // Jump Events
            OnJumpAction = null;
            OnJumpActionCanceled = null;

            // Interact Events
            OnInteractAction = null;
        }
    }
}
