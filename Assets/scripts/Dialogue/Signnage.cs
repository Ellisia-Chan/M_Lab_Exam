using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signnage : Interactible {

    [SerializeField] TextAsset inkJSON;

    protected override void Interact() {
        DialogueManager.Instance.EnterDialogueMode(inkJSON, null, null);
    }
}
