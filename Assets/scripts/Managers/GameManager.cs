using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    [SerializeField] private GameObject player;

    private Vector2 checkpointPos = Vector2.zero;
    private bool checkPointSet = false;

    public enum State {
        ALIVE,
        DEAD
    }

    public State currentState = State.ALIVE;


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

    public void SetCheckPoint(Transform position) {
        checkpointPos = position.position;
        checkPointSet = true;
    }

    public void RespawnPlayer() {
        if (checkPointSet) {
            player.transform.position = checkpointPos;
        } else {
            Debug.LogWarning("GameManager: No Checkpoint Set");
        }

        currentState = State.ALIVE;
    }
}
