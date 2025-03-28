using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : Interactible {
    protected override void Interact() {
        if (playerInRange) {
            Debug.Log("Interacted with " + gameObject.name);
        }
    }
}
