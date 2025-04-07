using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoinUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI coinText;

    private void Start() {
        UpdateCoinUI(new CoinValueChangeEvent(StatsManager.Instance.GetPlayerCoins()));
    }

    private void OnEnable() {
        EventBus.Subscribe<CoinValueChangeEvent>(UpdateCoinUI);

        if (StatsManager.Instance != null) {
            UpdateCoinUI(new CoinValueChangeEvent(StatsManager.Instance.GetPlayerCoins()));
        }
    }

    private void OnDisable() {
        EventBus.UnSubscribe<CoinValueChangeEvent>(UpdateCoinUI);

    }

    private void UpdateCoinUI(CoinValueChangeEvent e) {
        coinText.text = $"x {e.amount}";
    }
}
