using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrogUI : MonoBehaviour {
    [Header("Frog UI")]
    [SerializeField] private TextMeshProUGUI frogCountText;

    private void Start() {;
        UpdateFrogCountText(new FrogEventCountChange(StatsManager.Instance.GetFrogInteractedCount()));
    }

    private void OnEnable() {
        EventBus.Subscribe<FrogEventCountChange>(UpdateFrogCountText);
    }

    private void OnDisable() {
        EventBus.UnSubscribe<FrogEventCountChange>(UpdateFrogCountText);
    }

    private void UpdateFrogCountText(FrogEventCountChange e) {
        frogCountText.text = $"{e.count}/10";
    }

}
