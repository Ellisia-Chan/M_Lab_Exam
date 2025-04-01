using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>() != null) {
            EventManager.Instance.gameEvents.GameEnd();
        }
    }
}
