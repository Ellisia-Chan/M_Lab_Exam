using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    private Vector2 checkpointPos = Vector2.zero;
    //private bool checkPointSet = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("GameManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

    }
}
