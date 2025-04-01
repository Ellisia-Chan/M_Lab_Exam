using System;

public class CoinEvents {
    public event Action<int> OnCoinCollected;
    public event Action<int> OnCoinSpend;
    public event Action<int> OnCoinValueChange;

    public void CoinCollected(int amount) {
        OnCoinCollected?.Invoke(amount);
    }

    public void CoinSpend(int amount) => OnCoinSpend?.Invoke(amount);

    public void CoinValueChange(int amount) {
        OnCoinValueChange?.Invoke(amount);
    }


    public void Clear() {
        if (EventManager.Instance != null) {
            OnCoinCollected = null;
            OnCoinSpend = null;
            OnCoinValueChange = null; 
        }
    }
}
