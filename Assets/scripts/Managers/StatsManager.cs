using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    public static StatsManager Instance { get; private set; }

    private int coins = 0;
    private int interactedFrogs = 10;

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
        EventManager.Instance.frogEvents.OnFrogInteracted += HandleFrogInteracted;
        EventManager.Instance.coinEvents.OnCoinSpend += HandleCoinSpend;
    }

    private void OnDisable() {
        EventManager.Instance.coinEvents.OnCoinCollected -= HandleCoinCollected;
        EventManager.Instance.frogEvents.OnFrogInteracted -= HandleFrogInteracted;
        EventManager.Instance.coinEvents.OnCoinSpend -= HandleCoinSpend;
    }

    private void HandleCoinCollected(int amount) {
        coins += amount;
        EventManager.Instance.coinEvents.CoinValueChange(coins);
    }

    private void HandleCoinSpend(int amount) {
        coins -= amount;
        if (coins <= 0) {
            coins = 0;
        }
        EventManager.Instance.coinEvents.CoinValueChange(coins);
    }

    private void HandleFrogInteracted() {
        interactedFrogs++;
        EventManager.Instance.frogEvents.FrogCountChange(interactedFrogs);
    }

    public int GetPlayerCoins() => coins;
    public int GetFrogInteractedCount() => interactedFrogs;
}
