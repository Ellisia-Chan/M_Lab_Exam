using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [Header("Stats")]
    //[SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject statsUI;

    [Header("Dialogue")]
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject choiceUI;

    [Header("DeadUI")]
    [SerializeField] private GameObject deadUI;

    [Header("CheckpointUI")]
    [SerializeField] private GameObject CheckpointUI;

    [Header("GameOver UI")]
    [SerializeField] private GameObject GameOverUI;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("UIManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        EventBus.Subscribe<GameEventStateEnd>(e => ShowGameOverUI());
        EventBus.Subscribe<PlayerEventDeath>(e => ShowDeadUI());
        EventBus.Subscribe<PlayerEventRespawn>(e => HideDeadUI());
    }

    private void OnDisable() {
        EventBus.UnSubscribe<GameEventStateEnd>(e => ShowGameOverUI());
        EventBus.UnSubscribe<PlayerEventDeath>(e => ShowDeadUI());
        EventBus.UnSubscribe<PlayerEventRespawn>(e => HideDeadUI());
    }

    // Stats UI
    public void ToggleStatsUI(bool state) => statsUI.SetActive(state);

    // Dialogue UI
    public void ToggleDialogueUI(bool state) => dialogueUI.SetActive(state);
    public void ToggleDialogueChoiceUI(bool state) => choiceUI.SetActive(state);

    // Dead UI
    public void ToggleDeadUI(bool state) {
        deadUI.SetActive(state);

        if (state) {
            statsUI.SetActive(false);
        } else {
            statsUI.SetActive(true);
        }
    }

    // Checkpoint UI
    public void ToggleCheckpointUI(bool state) => CheckpointUI.SetActive(state);

    // GameOver UI
    public void ToggleGameOverUI(bool state) {
        GameOverUI.SetActive(state);
        statsUI.SetActive(false);
    }

    private void ShowDeadUI() { ToggleDeadUI(true); }
    private void HideDeadUI() { ToggleDeadUI(false); }
    private void ShowGameOverUI() { ToggleGameOverUI(true); }
    private void HideGameOverUI() { ToggleGameOverUI(false); }

}
