using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private GameObject player;

    private Vector2 checkpointPos = Vector2.zero;
    private bool checkPointSet = false;
    private int gameTime;

    public enum State {
        ALIVE,
        DEAD
    }

    public enum GameState {
        ISGAMEPLAYING,
        GAMEOVER
    }


    /// <summary>
    /// The current state of the player and the game.
    /// </summary>
    public State currentState = State.ALIVE;
    public GameState gameState = GameState.ISGAMEPLAYING;


    /// <summary>
    /// Called when the script is enabled. This function is used to initialize the singleton instance, 
    /// set the initial checkpoint position, and start the game timer.
    /// </summary>
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("GameManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

        checkpointPos = player.transform.position;
        checkPointSet = true;
    }


    /// <summary>
    /// Called at the start of the game. Triggers the GameStart and GameStateChange events if the game is in the ISGAMEPLAYING state.
    /// </summary>
    private void Start() {
        if (gameState == GameState.ISGAMEPLAYING) {
            EventManager.Instance.gameEvents.GameStart();
            EventManager.Instance.gameEvents.GameStateChange();
        }
    }

    private void Update() {
        if (gameState == GameState.ISGAMEPLAYING) {
            gameTime = (int)Time.time;
        }
    }


    /// <summary>
    /// Called when the script is enabled. This function is used to subscribe to events that are triggered by other scripts.
    /// </summary>
    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerRespawn += RespawnPlayerState;
            EventManager.Instance.gameEvents.OnGameStateEnd += SetGameOverState;
        }
    }


    /// <summary>
    /// Called when the script is disabled. This function is used to unsubscribe from events that are triggered by other scripts.
    /// </summary>
    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.OnPlayerRespawn -= RespawnPlayerState;
            EventManager.Instance.gameEvents.OnGameStateEnd -= SetGameOverState;
        }
    }


    /// <summary>
    /// Sets the checkpoint position and sets the checkPointSet flag to true.
    /// </summary>
    /// <param name="position"></param>
    public void SetCheckPoint(Transform position) {
        checkpointPos = position.position;
        checkPointSet = true;
    }


    /// <summary>
    /// Respawns the player by setting their position to the checkpoint position.
    /// </summary>
    public void RespawnPlayer() {
        if (checkPointSet) {
            player.transform.position = checkpointPos;
        } else {
            Debug.LogWarning("GameManager: No Checkpoint Set");
        }

    }


    /// <summary>
    /// Resets the player state to ALIVE.
    /// </summary>
    private void RespawnPlayerState() {
        currentState = State.ALIVE;
    }


    /// <summary>
    /// Sets the game state to GAMEOVER and triggers the GameStateChange event.
    /// </summary>
    private void SetGameOverState() {
        gameState = GameState.GAMEOVER;
        EventManager.Instance.gameEvents.GameStateChange();
    }


    /// <summary>
    /// Returns the current game time in seconds.
    /// </summary>
    /// <returns></returns>
    public int GetTimer() {
        return gameTime;
    }
}
