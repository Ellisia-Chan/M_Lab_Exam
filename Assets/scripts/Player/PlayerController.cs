using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float cameraShakeDuration = 0.5f;
    [SerializeField] private float cameraShakeIntensity = 0.5f;
    [SerializeField] private float damageColorDuration = 0.2f;

    [Header("Dialogue InkJSON")]
    [SerializeField] private TextAsset playerInkJSON;
    [SerializeField] private float dialogueStartDelay = 0.8f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private PlayerMovement playerMovement;

    private int health = 100;
    private bool playerDialoguePlayed = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("PlayerController: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable() {
        EventBus.Subscribe<DialogueEndEvent>(e => HandlePlayerDialogue());
        EventBus.Subscribe<PlayerEventDamageHit>(HandleDamage);
        EventBus.Subscribe<PlayerEventRespawn>(e => PlayerHealthChange());
        EventBus.Subscribe<PlayerInputEventInteract>(e => HandleInteract());
    }

    private void OnDisable() {
        EventBus.UnSubscribe<DialogueEndEvent>(e => HandlePlayerDialogue());
        EventBus.UnSubscribe<PlayerEventDamageHit>(HandleDamage);
        EventBus.UnSubscribe<PlayerEventRespawn>(e => PlayerHealthChange());
        EventBus.UnSubscribe<PlayerInputEventInteract>(e => HandleInteract());
    }

    private void Start() {
        EventBus.Publish(new PlayerEventHealthChange(health));
    }


    private void HandleInteract() {
        //Debug.Log("Interact");
    }


    /// <summary>
    /// Checks the player's health and performs necessary actions.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void CheckHealth() {
        EventBus.Publish(new PlayerEventHealthChange(health));

        if (health <= 0) {
            Debug.Log("Dead");
            Die();
        }
    }


    /// <summary>
    /// Handles the player taking damage, applying the damage to the player's health and triggering a camera shake effect.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to the player's health.</param>
    public void HandleDamage(PlayerEventDamageHit e) {
        if (CameraShake.Instance != null) CameraShake.Instance.Shake(cameraShakeIntensity, cameraShakeDuration);

        health -= e.damage;
        CheckHealth();

        StartCoroutine(FlashRedOnDamage());
    }


    /// <summary>
    /// Flashes the player's sprite red for a specified duration to indicate damage taken.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private IEnumerator FlashRedOnDamage() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageColorDuration);
        spriteRenderer.color = originalColor;
    }


    /// <summary>
    /// Handles the player's death, updating the game state and triggering a respawn sequence.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void Die() {
        GameManager.Instance.currentState = GameManager.State.DEAD;
        EventBus.Publish(new PlayerEventDeath());
        rb.velocity = Vector2.zero;
        StartCoroutine(RespawnSequence());
    }


    /// <summary>
    /// Resets the player's state after a death, waiting for a specified duration before respawning.
    /// 
    /// Parameters: None
    /// Returns: IEnumerator
    /// </summary>
    private IEnumerator RespawnSequence() {
        yield return new WaitForSecondsRealtime(3f);

        health = 100;

        GameManager.Instance.RespawnPlayer();
    }


    /// <summary>
    /// Updates the player's health by notifying the EventManager.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void PlayerHealthChange() {
        EventBus.Publish(new PlayerEventHealthChange(health));
    }


    /// <summary>
    /// Handles the player's dialogue after a certain number of interactions.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void HandlePlayerDialogue() {
        if (StatsManager.Instance.GetFrogInteractedCount() >= 10 && !playerDialoguePlayed) {
            playerDialoguePlayed = true;
            StartCoroutine(StartPlayerDialogue());
        }
    }


    /// <summary>
    /// Starts the player's dialogue after a delay.
    /// 
    /// Parameters: None
    /// Returns: IEnumerator
    /// </summary>
    private IEnumerator StartPlayerDialogue() {
        yield return new WaitForSeconds(dialogueStartDelay);

        while (DialogueManager.Instance.IsDialoguePlaying() || !playerMovement.IsGrounded()) {
            yield return null;
        }
        DialogueManager.Instance.EnterDialogueMode(playerInkJSON, null, null);
    }

}
