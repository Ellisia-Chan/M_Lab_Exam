using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterNumFrog : MonoBehaviour {
    [Header("Dialogue Data")]
    [SerializeField] private TextAsset completeFrogJSON;

    [Header("PathBlock")]
    [SerializeField] private GameObject pathBlock;

    private DialogueTrigger dialogueTrigger;

    private void Awake() {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void Start() {
        CheckFrogCount(StatsManager.Instance.GetFrogInteractedCount());
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.frogEvents.OnFrogCountChange += CheckFrogCount;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.frogEvents.OnFrogCountChange -= CheckFrogCount;
        }
    }

    private void CheckFrogCount(int count) {
        if (count >= 10) {
            dialogueTrigger.SetInkJSON(completeFrogJSON);
            pathBlock.SetActive(false);
        }
    }
}
