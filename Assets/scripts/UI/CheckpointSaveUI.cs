using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSaveUI : MonoBehaviour {
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        animator.Play("CheckpointSaveUIAnim");
    }

    private void OnAnimFinish() {
        UIManager.Instance.ToggleCheckpointUI(false);
    }
}
