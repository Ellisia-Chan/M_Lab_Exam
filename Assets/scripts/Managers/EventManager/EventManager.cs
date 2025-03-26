using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager Instance { get; private set; }

    public PlayerInputEvents playerInputEvents { get; private set; }

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
    }

    private void UnSubscribeEvents() {
        if (Instance != null) {
            playerInputEvents.Clear();
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            UnSubscribeEvents();
            Instance = null;
        }
    }

}