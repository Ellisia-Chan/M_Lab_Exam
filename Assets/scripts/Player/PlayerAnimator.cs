using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    private const string X_DIR = "X_Dir";
    private const string IS_MOVING = "isMoving";

    private Animator animator;
    private Vector2 moveDir;
    private Vector2 lastMoveDir;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (animator != null) {
            if (GameInputManager.Instance != null && !DialogueManager.Instance.IsDialoguePlaying() && GameManager.Instance.currentState == GameManager.State.ALIVE) {
                moveDir = GameInputManager.Instance.GetMovementVectorNormalize();

                if (moveDir != Vector2.zero) {
                    lastMoveDir = moveDir;
                }

                animator.SetFloat(X_DIR, lastMoveDir.x);
                animator.SetBool(IS_MOVING, moveDir != Vector2.zero);
            } else {
                if (GameManager.Instance.currentState == GameManager.State.DEAD) {
                    animator.SetBool(IS_MOVING, false);
                }
            }

        }
    }
}
