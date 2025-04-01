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
    }

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction += HandleInteract;
            EventManager.Instance.playerEvents.OnPlayerHit += HandleDamage;
            EventManager.Instance.playerEvents.OnPlayerRespawn += PlayerHealthChange;
            EventManager.Instance.dialogueEvents.OnDialogueEnd += HandlePlayerDialogue;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnInteractAction -= HandleInteract;
            EventManager.Instance.playerEvents.OnPlayerHit -= HandleDamage;
            EventManager.Instance.playerEvents.OnPlayerRespawn -= PlayerHealthChange;
            EventManager.Instance.dialogueEvents.OnDialogueEnd -= HandlePlayerDialogue;

        }
    }

    private void Start() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.PlayerHealthChange(health); 
        }
    }


    private void HandleInteract() {
        //Debug.Log("Interact");
    }


    private void CheckHealth() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.PlayerHealthChange(health);
        }

        if (health <= 0) {
            Debug.Log("Dead");
            Die();
        }
    }

    public void HandleDamage(int damage) {
        if (CameraShake.Instance != null) CameraShake.Instance.Shake(cameraShakeIntensity, cameraShakeDuration);

        health -= damage;
        CheckHealth();

        StartCoroutine(FlashRedOnDamage());
    }

    private IEnumerator FlashRedOnDamage() {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(damageColorDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die() {
        GameManager.Instance.currentState = GameManager.State.DEAD;
        EventManager.Instance.playerEvents.PlayerDeath();
        rb.velocity = Vector2.zero;
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence() {
        yield return new WaitForSecondsRealtime(3f);

        health = 100;

        GameManager.Instance.RespawnPlayer();
    }

    private void PlayerHealthChange() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerEvents.PlayerHealthChange(health);
        }
    }

    private void HandlePlayerDialogue() {
        if (StatsManager.Instance.GetFrogInteractedCount() >= 10 && !playerDialoguePlayed) {
            playerDialoguePlayed = true;
            StartCoroutine(StartPlayerDialogue());
        }
    }

    private IEnumerator StartPlayerDialogue() {
        yield return new WaitForSeconds(dialogueStartDelay);
        if (!DialogueManager.Instance.IsDialoguePlaying()) {
            DialogueManager.Instance.EnterDialogueMode(playerInkJSON, null, null);
        }
    }
}
