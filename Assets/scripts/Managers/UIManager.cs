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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("UIManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerDeath += ShowDeadUI;
            EventManager.Instance.playerEvents.OnPlayerRespawn += HideDeadUI;

        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerDeath -= ShowDeadUI;
            EventManager.Instance.playerEvents.OnPlayerRespawn -= HideDeadUI;

        }
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

    private void ShowDeadUI() { ToggleDeadUI(true); }
    private void HideDeadUI() { ToggleDeadUI(false); }

}
