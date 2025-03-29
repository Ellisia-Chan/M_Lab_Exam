using System;

public class DialogueEvents {
    public event Action OnDialogueStart;
    public event Action OnDialogueEnd;

    public void DialogueStart() => OnDialogueStart?.Invoke();
    public void DialogueEnd() => OnDialogueEnd?.Invoke();

    public void Clear() {
        if (EventManager.Instance != null) {
            OnDialogueStart = null;
            OnDialogueEnd = null;
        }
    }
}
