using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerController>() != null) {
            EventManager.Instance.playerEvents.PlayerHit(100);
            Debug.Log("Player Died");
        }
    }
}
