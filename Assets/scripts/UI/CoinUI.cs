using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI coinText;

    private void Start() {
        UpdateCoinUI(StatsManager.Instance.GetPlayerCoins());
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.coinEvents.OnCoinValueChange += UpdateCoinUI;
        }

        if (StatsManager.Instance != null) {
            UpdateCoinUI(StatsManager.Instance.GetPlayerCoins());
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.coinEvents.OnCoinValueChange -= UpdateCoinUI;
        }
    }


    /// <summary>
    /// Updates the Coin UI text to display the specified amount.
    /// </summary>
    /// <param name="amount">The amount of coins to display.</param>
    private void UpdateCoinUI(int amount) {
        coinText.text = $"x {amount}";
    }
}
