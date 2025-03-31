using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    public static StatsManager Instance { get; private set; }

    private int coins = 0;
    private int diamonds = 0;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("StatsManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        EventManager.Instance.coinEvents.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable() {
        EventManager.Instance.coinEvents.OnCoinCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(int amount) {
        coins += amount;
        EventManager.Instance.coinEvents.CoinValueChange(coins);
    }

    public int GetPlayerCoins() => coins;
}
