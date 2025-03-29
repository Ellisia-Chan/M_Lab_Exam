using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactible {

    [Header("Dialogue Data")]
    [SerializeField] private TextAsset inkJSON;

    [SerializeField] private GameObject speakerArrow;

    protected override void Interact() {
        if (!DialogueManager.Instance.IsDialoguePlaying()) {
            DialogueManager.Instance.EnterDialogueMode(inkJSON, speakerArrow);
        }
    }
}
