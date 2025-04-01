using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrogUI : MonoBehaviour {
    [Header("Frog UI")]
    [SerializeField] private TextMeshProUGUI frogCountText;

    private void Start() {;
        UpdateFrogCountText(StatsManager.Instance.GetFrogInteractedCount());
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.frogEvents.OnFrogCountChange += UpdateFrogCountText;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.frogEvents.OnFrogCountChange -= UpdateFrogCountText;
        }
    }

    private void UpdateFrogCountText(int amount) {
        frogCountText.text = $"{amount}/10";
    }

}
