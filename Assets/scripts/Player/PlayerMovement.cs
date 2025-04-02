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
    [SerializeField] private float knockbackDuration = 0.3f;

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
            EventManager.Instance.dialogueEvents.OnDialogueStart += FreezePosition;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnJumpAction -= HandleJump;
            EventManager.Instance.dialogueEvents.OnDialogueStart -= FreezePosition;
        }
    }

    private void Update() {
        HandlePlayerInput();
        HandleGroundCheck();
    }

    private void FixedUpdate() {
        HandleMovement();
    }


    /// <summary>
    /// Handles the player's input by retrieving the normalized movement vector from the GameInputManager.
    /// </summary>
    private void HandlePlayerInput() {
        if (GameInputManager.Instance != null) {
            moveVector = GameInputManager.Instance.GetMovementVectorNormalize();
        }
    }


    /// <summary>
    /// Handles the player's movement by checking the game state, dialogue status, and knockback state.
    /// If all conditions are met, updates the player's velocity based on the movement vector and speed.
    /// </summary>
    private void HandleMovement() {
        if (GameManager.Instance.gameState == GameManager.GameState.ISGAMEPLAYING && GameManager.Instance.currentState == GameManager.State.ALIVE) {
            if (!DialogueManager.Instance.IsDialoguePlaying()) {
                if (!isKnockedBack) {
                    rb.velocity = new Vector2(moveVector.x * moveSpeed, rb.velocity.y);
                }
            } 
        }
    }


    /// <summary>
    /// Handles the player's jump action by checking the game state, dialogue status, and grounded state.
    /// If all conditions are met, applies a jump force to the player's velocity.
    /// </summary>
    private void HandleJump() {
        if (GameManager.Instance.gameState == GameManager.GameState.ISGAMEPLAYING && GameManager.Instance.currentState == GameManager.State.ALIVE) {
            if (!DialogueManager.Instance.IsDialoguePlaying()) {
                if (isGrounded && !isKnockedBack) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            } 
        }
    }


    /// <summary>
    /// Checks if the player is grounded by casting a circle overlap at the ground check position.
    /// </summary>
    private void HandleGroundCheck() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }


    /// <summary>
    /// Applies a knockback force to the player, setting their velocity to zero and then adding the specified force.
    /// </summary>
    /// <param name="force">The force to apply to the player.</param>
    public void ApplyKnockBack(Vector2 force) {
        if (rb != null) {
            isKnockedBack = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(ResetKnockback());
        }
    }


    /// <summary>
    /// Resets the knockback state of the player after a certain duration.
    /// </summary>
    /// <returns>An IEnumerator that yields control for the duration of the knockback.</returns>
    private IEnumerator ResetKnockback() {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }


    /// <summary>
    /// Immediately stops the object from moving by setting its velocity to zero.
    /// </summary>
    private void FreezePosition() {
        if (rb != null) {
            rb.velocity = Vector2.zero;
        }
    }

    public bool IsGrounded() => isGrounded;

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

#endif

}
