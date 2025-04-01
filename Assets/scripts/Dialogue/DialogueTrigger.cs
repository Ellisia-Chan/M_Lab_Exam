using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactible {

    [Header("Dialogue Data")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject speakerArrow;
    [SerializeField] private string npcID;

    private TextAsset ink;

    private void Awake() {
        if (npcID == "" || npcID == null) {
            npcID = gameObject.name;
        }

        if (inkJSON != null) {
            ink = inkJSON;
        }
    }

    protected override void Interact() {
        if (!DialogueManager.Instance.IsDialoguePlaying() && ink != null && speakerArrow != null) {
            DialogueManager.Instance.EnterDialogueMode(ink, speakerArrow, npcID);
        }
    }

    // This is used when Dialogue has multipl ink files conditions
    public void SetInkJSON(TextAsset textAsset) {
        ink = textAsset;
    }
}
