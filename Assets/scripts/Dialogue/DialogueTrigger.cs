using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactible {

    [Header("Dialogue Data")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject speakerArrow;
    [SerializeField] private string npcID;

    private void Awake() {
        npcID = gameObject.name;
    }

    protected override void Interact() {
        if (!DialogueManager.Instance.IsDialoguePlaying()) {
            DialogueManager.Instance.EnterDialogueMode(inkJSON, speakerArrow, npcID);
        }
    }
}
