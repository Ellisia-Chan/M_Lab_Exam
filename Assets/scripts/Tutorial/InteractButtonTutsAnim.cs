using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButtonTutsAnim : MonoBehaviour {
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        animator.Play("InteractButton");
    }
}
