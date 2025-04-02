using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {
    [SerializeField] private float knockbackForce = 10f;
    [SerializeField] private int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player != null && !DialogueManager.Instance.IsDialoguePlaying()) {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 knockbackDirection = ((Vector2)contact.point - (Vector2)player.transform.position).normalized;
            Vector2 knockbackForceVector = -knockbackDirection * knockbackForce;
            player.ApplyKnockBack(knockbackForceVector);

            EventManager.Instance.playerEvents.PlayerHit(damage);
        }
    }
}
