using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    public static StatsManager Instance { get; private set; }

    private int score = 0;
    private int coins = 0;
    private int interactedFrogs = 0;


    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes the StatsManager instance and ensures only one instance exists.
    /// </summary>
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("StatsManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }


    private void OnEnable() {
        EventBus.Subscribe<CoinCollectedEvent>(HandleCoinCollected);
        EventBus.Subscribe<CoinSpendEvent>(HandleCoinSpend);
        EventBus.Subscribe<FrogEventInteracted>(e => HandleFrogInteracted());
    }


    private void OnDisable() {
        EventBus.UnSubscribe<CoinCollectedEvent>(HandleCoinCollected);
        EventBus.UnSubscribe<FrogEventInteracted>(e => HandleFrogInteracted());

    }



    private void HandleCoinCollected(CoinCollectedEvent e) {
        // Update the score and coin count
        score += e.amount;
        coins += e.amount;

        EventBus.Publish(new CoinValueChangeEvent(coins));
    }


    private void HandleCoinSpend(CoinSpendEvent e) {
        // Subtract the spent amount from the total coins
        coins -= e.amount;

        // Ensure the coin count does not go below 0
        if (coins <= 0) {
            coins = 0;
        }

        EventBus.Publish(new CoinValueChangeEvent(coins));
    }


    /// <summary>
    /// Handles the event when a frog is interacted with.
    /// </summary>
    private void HandleFrogInteracted() {
        // Increment the interacted frogs count
        interactedFrogs++;
        
        EventBus.Publish(new FrogEventCountChange(interactedFrogs));
    }


    public int GetPlayerCoins() => coins;
    public int GetFrogInteractedCount() => interactedFrogs;
    public int GetScore() => score;
}
