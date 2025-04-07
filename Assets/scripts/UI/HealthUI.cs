using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    [Header("Health UI")]
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable() {
        EventBus.Subscribe<PlayerEventHealthChange>(UpdateHealthUI);
    }

    private void OnDisable() {
        EventBus.UnSubscribe<PlayerEventHealthChange>(UpdateHealthUI);
    }

    private void UpdateHealthUI(PlayerEventHealthChange e) {
        healthBarFill.fillAmount = e.health / 100f;

        if (e.health >= 0) {
            healthText.text = $"{e.health}";
        } else {
            healthText.text = "0";
        }
    }
}
