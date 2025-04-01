using System;

public class GameEvents {
    public event Action OnGameStateStart;
    public event Action OnGameStateEnd;
    public event Action OnGameStateChange;

    public void GameStart() => OnGameStateStart?.Invoke();
    public void GameEnd() => OnGameStateEnd?.Invoke();
    public void GameStateChange() => OnGameStateChange?.Invoke();

    public void Clear() {
        if (EventManager.Instance != null) {
            OnGameStateChange = null;
            OnGameStateEnd = null;
            OnGameStateStart = null;
        }
    }
}
