using System;

public class UIEvents {
    public event Action OnDeadUIAnimStart;
    public event Action OnDeadUIAnimStop;

    public void DeadUIAnimStart() => OnDeadUIAnimStart?.Invoke();
    public void DeadUIAnimStop() => OnDeadUIAnimStop?.Invoke();

    public void Clear() {
        if (EventManager.Instance != null) {
            OnDeadUIAnimStart = null;
            OnDeadUIAnimStop = null;
        }
    }
}
