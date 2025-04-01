using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonTuts : MonoBehaviour {

    [SerializeField] private GameObject interactButton;
    private bool isShown = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>() != null) {
            if (!isShown) {
                isShown = true;
                interactButton.SetActive(true);
            }
        }
    }
}
