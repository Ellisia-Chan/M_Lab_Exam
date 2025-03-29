using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    private Rigidbody2D rb;

    private int health = 100;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("PlayerController: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction += HandleInteract;
            EventManager.Instance.playerEvents.OnPlayerHit += HandleDamage;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction -= HandleInteract;
            EventManager.Instance.playerEvents.OnPlayerHit -= HandleDamage;
        }
    }


    private void HandleInteract() {
        //Debug.Log("Interact");
    }


    private void CheckHealth() {
        Debug.Log("Health: " + health);
        if (health <= 0) {
            Debug.Log("Dead");
            Die();
        }
    }

    public void HandleDamage(int damage) {
        if (CameraShake.Instance != null) CameraShake.Instance.Shake(6f, 0.5f);

        health -= damage;
        CheckHealth();
    }

    private void Die() {
        GameManager.Instance.currentState = GameManager.State.DEAD;
        rb.velocity = Vector2.zero;
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence() {
        yield return new WaitForSeconds(2f);

        health = 100;
        GameManager.Instance.RespawnPlayer();
    }

}
