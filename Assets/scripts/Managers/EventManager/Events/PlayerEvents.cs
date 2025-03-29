using System;

public class PlayerEvents {
    public event Action<int> OnPlayerHit;

    public void PlayerHit(int amount) => OnPlayerHit?.Invoke(amount);

    public void Clear() {
        if (EventManager.Instance != null) {
            OnPlayerHit = null;
        }
    }
}
