using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactible : MonoBehaviour {

    [Header("Interactible")]
    [SerializeField] protected GameObject interactPrompt;

    protected bool playerInRange = false;

    private void Start() {
        if (interactPrompt != null) {
            interactPrompt.SetActive(false);
        }
    }

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

    protected abstract void HandleInteract();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerController>() != null) {
            playerInRange = true;

            if (interactPrompt != null) {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.GetComponent<PlayerController>() != null) {
            playerInRange = false;

            if (interactPrompt != null) {
                interactPrompt.SetActive(false);
            }
        }
    }
}
