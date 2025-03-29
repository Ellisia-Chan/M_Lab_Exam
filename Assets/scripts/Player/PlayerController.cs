using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    //private int health = 100;

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction += HandleInteract;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction -= HandleInteract;
        }
    }


    private void HandleInteract() {
        Debug.Log("Interact");
    }

}
