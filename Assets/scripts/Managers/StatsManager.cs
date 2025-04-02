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


    /// <summary>
    /// Called when the script is enabled. 
    /// This function is used to subscribe to events that are triggered by other scripts.
    /// </summary>
    private void OnEnable() {
        EventManager.Instance.coinEvents.OnCoinCollected += HandleCoinCollected;
        EventManager.Instance.frogEvents.OnFrogInteracted += HandleFrogInteracted;
        EventManager.Instance.coinEvents.OnCoinSpend += HandleCoinSpend;
    }

    /// <summary>
    /// Handles the event when the StatsManager is disabled.
    /// Removes event listeners for coin collection, frog interaction, and coin spending.
    /// </summary>
    private void OnDisable() {
        EventManager.Instance.coinEvents.OnCoinCollected -= HandleCoinCollected;
        EventManager.Instance.frogEvents.OnFrogInteracted -= HandleFrogInteracted;
        EventManager.Instance.coinEvents.OnCoinSpend -= HandleCoinSpend;
    }


    /// <summary>
    /// Handles the event when a coin is collected.
    /// </summary>
    /// <param name="amount">The amount of coins collected.</param>
    private void HandleCoinCollected(int amount) {
        // Update the score and coin count
        score += amount;
        coins += amount;

        // Notify the EventManager of the coin value change
        EventManager.Instance.coinEvents.CoinValueChange(coins);
    }


    /// <summary>
    /// Handles the event when a coin is spent.
    /// </summary>
    /// <param name="amount">The amount of coins spent.</param>
    private void HandleCoinSpend(int amount) {
        Debug.Log("Spent " + amount + " coins.");
        // Subtract the spent amount from the total coins
        coins -= amount;

        // Ensure the coin count does not go below 0
        if (coins <= 0) {
            coins = 0;
        }

        // Notify the EventManager of the coin value change
        EventManager.Instance.coinEvents.CoinValueChange(coins);
    }


    /// <summary>
    /// Handles the event when a frog is interacted with.
    /// </summary>
    private void HandleFrogInteracted() {
        // Increment the interacted frogs count
        interactedFrogs++;
        // Notify the EventManager of the frog count change
        EventManager.Instance.frogEvents.FrogCountChange(interactedFrogs);
    }


    public int GetPlayerCoins() => coins;
    public int GetFrogInteractedCount() => interactedFrogs;
    public int GetScore() => score;
}
