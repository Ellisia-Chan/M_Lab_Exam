using System;

public class PlayerEvents {
    public event Action<int> OnPlayerHit;
    public event Action OnPlayerDeath;
    public event Action OnPlayerRespawn;

    public void PlayerHit(int amount) => OnPlayerHit?.Invoke(amount);
    public void PlayerDeath() => OnPlayerDeath?.Invoke();
    public void PlayerRespawn() => OnPlayerRespawn?.Invoke();

    public void Clear() {
        if (EventManager.Instance != null) {
            OnPlayerHit = null;
            OnPlayerDeath = null;
            OnPlayerRespawn = null;
        }
    }
}
