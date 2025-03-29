using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactible {
    protected override void Interact() {
        GameManager.Instance.SetCheckPoint(transform);
        Debug.Log("Checkpoint Set: " + transform.position);
    }
}
