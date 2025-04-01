using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static EventManager Instance { get; private set; }

    public PlayerEvents playerEvents { get; private set; }
    public PlayerInputEvents playerInputEvents { get; private set; }
    public CoinEvents coinEvents { get; private set; }
    public DialogueEvents dialogueEvents { get; private set; }
    public UIEvents uiEvents { get; private set; }
    public FrogEvents frogEvents { get; private set; }
    public GameEvents gameEvents { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            InitializeEvents();
        } else {
            Debug.LogWarning("EventManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// Initializes all the events in the EventManager.
    /// </summary>
    private void InitializeEvents() {
        playerInputEvents = new PlayerInputEvents();
        coinEvents = new CoinEvents();
        dialogueEvents = new DialogueEvents();
        playerEvents = new PlayerEvents();
        uiEvents = new UIEvents();
        frogEvents = new FrogEvents();
        gameEvents = new GameEvents();
    }


    /// <summary>
    /// Unsubscribes all events in the EventManager.
    /// </summary>
    private void UnSubscribeEvents() {
        if (Instance != null) {
            playerInputEvents.Clear();
            coinEvents.Clear();
            dialogueEvents.Clear();
            playerEvents.Clear();
            uiEvents.Clear();
            frogEvents.Clear();
            gameEvents.Clear();
        }
    }


    /// <summary>
    /// Called when the EventManager object is destroyed.
    /// Ensures that all events are unsubscribed and the instance is reset.
    /// </summary>
    private void OnDestroy() {
        if (Instance == this) {
            UnSubscribeEvents();
            Instance = null;
        }
    }

}