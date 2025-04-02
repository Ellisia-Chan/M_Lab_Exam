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


    /// <summary>
    /// Interacts with the NPC by entering dialogue mode if the dialogue is not already playing and the required components are available.
    /// </summary>
    protected override void Interact() {
        if (!DialogueManager.Instance.IsDialoguePlaying() && ink != null && speakerArrow != null) {
            DialogueManager.Instance.EnterDialogueMode(ink, speakerArrow, npcID);
        }
    }


    /// <summary>
    /// Sets the ink JSON asset for the dialogue trigger.
    /// </summary>
    /// <param name="textAsset">The text asset containing the ink JSON data.</param>
    public void SetInkJSON(TextAsset textAsset) {
        ink = textAsset;
    }
}
