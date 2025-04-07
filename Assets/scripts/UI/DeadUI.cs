using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUI : MonoBehaviour {
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        animator.Play("ShowDeadUI");
    }

    private void OnAnimFinish() {
        EventBus.Publish(new PlayerEventRespawn());
    }
}
