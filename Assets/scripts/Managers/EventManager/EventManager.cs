using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager Instance { get; private set; }

    public PlayerEvents playerEvents { get; private set; }
    public PlayerInputEvents playerInputEvents { get; private set; }
    public CoinEvents coinEvents { get; private set; }
    public DialogueEvents dialogueEvents { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            InitializeEvents();
        } else {
            Debug.LogWarning("EventManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    private void InitializeEvents() {
        playerInputEvents = new PlayerInputEvents();
        coinEvents = new CoinEvents();
        dialogueEvents = new DialogueEvents();
        playerEvents = new PlayerEvents();
    }

    private void UnSubscribeEvents() {
        if (Instance != null) {
            playerInputEvents.Clear();
            coinEvents.Clear();
            dialogueEvents.Clear();
            playerEvents.Clear();
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            UnSubscribeEvents();
            Instance = null;
        }
    }

}