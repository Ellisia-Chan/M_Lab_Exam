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
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerHealthChange += UpdateHealthUI;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerHealthChange -= UpdateHealthUI;
        }
    }

    private void UpdateHealthUI(int healthValue) {
        healthBarFill.fillAmount = healthValue / 100f;

        if (healthValue >= 0) {
            healthText.text = $"{healthValue}";
        } else {
            healthText.text = "0";
        }
    }
}
