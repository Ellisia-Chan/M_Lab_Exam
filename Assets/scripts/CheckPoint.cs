using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactible {
    protected override void Interact() {
        GameManager.Instance.SetCheckPoint(transform);
        UIManager.Instance.ToggleCheckpointUI(true);
        Debug.Log("Checkpoint Set: " + transform.position);
    }
}
