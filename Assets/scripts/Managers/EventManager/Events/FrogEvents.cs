using System;

public class FrogEvents {
    public event Action OnFrogInteracted;
    public event Action<int> OnFrogCountChange;

    public void FrogInteracted() => OnFrogInteracted?.Invoke();
    public void FrogCountChange(int amount) => OnFrogCountChange?.Invoke(amount);

    public void Clear() {
        if (EventManager.Instance != null) {
            OnFrogInteracted = null;
        }
    }
}
