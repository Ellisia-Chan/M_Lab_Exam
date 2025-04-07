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
        CheckFrogCount(new FrogEventCountChange(StatsManager.Instance.GetFrogInteractedCount()));
    }

    private void OnEnable() {
        EventBus.Subscribe<FrogEventCountChange>(CheckFrogCount);
    }

    private void OnDisable() {
        EventBus.UnSubscribe<FrogEventCountChange>(CheckFrogCount);
    }


    /// <summary>
    /// Checks if the frog count has reached a certain threshold and performs actions accordingly.
    /// </summary>
    /// <param name="count">The current frog count.</param>
    private void CheckFrogCount(FrogEventCountChange e) {
        if (e.count>= 10) {
            dialogueTrigger.SetInkJSON(completeFrogJSON);
            pathBlock.SetActive(false);
        }
    }
}
