using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 3f;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Damage")]
    [SerializeField] private float knockbackDuration = 0.5f;

    private Rigidbody2D rb;

    private Vector2 moveVector;
    private bool isGrounded;
    private bool isKnockedBack = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnJumpAction += HandleJump;
        }
    }

    private void Update() {
        HandlePlayerInput();
        HandleGroundCheck();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void HandlePlayerInput() {
        if (GameInputManager.Instance != null) {
            moveVector = GameInputManager.Instance.GetMovementVectorNormalize();
        }
    }

    private void HandleMovement() {
        if (!DialogueManager.Instance.IsDialoguePlaying() && GameManager.Instance.currentState == GameManager.State.ALIVE) {
            if (!isKnockedBack) {
                rb.velocity = new Vector2(moveVector.x * moveSpeed, rb.velocity.y); 
            }
        }
    }

    private void HandleJump() {
        if (!DialogueManager.Instance.IsDialoguePlaying() && GameManager.Instance.currentState == GameManager.State.ALIVE) {
            if (isGrounded && !isKnockedBack) {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void HandleGroundCheck() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public void ApplyKnockBack(Vector2 force) {
        if (rb != null) {
            isKnockedBack = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback() {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

#endif

}
