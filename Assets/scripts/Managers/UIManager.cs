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

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("UIManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    // Stats UI
    public void ToggleStatsUI(bool state) => statsUI.SetActive(state);

    // Dialogue UI
    public void ToggleDialogueUI(bool state) => dialogueUI.SetActive(state);
    public void ToggleDialogueChoiceUI(bool state) => choiceUI.SetActive(state);
}
