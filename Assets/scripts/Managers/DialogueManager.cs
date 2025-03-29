using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance { get; private set; }

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextMeshProUGUI[] choiceTexts;

    [Header("Player Arrow")]
    [SerializeField] private GameObject playerArrow;

    private Story currentStory;
    private bool isDialoguePlaying = false;
    private bool isDisplayingChoices = false;

    private GameObject speakerArrow;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("DialogueManager: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }
    }

    public static DialogueManager GetInstance() => Instance;

    private void OnEnable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnContinueAction += ContinueStory;
        }
    }

    private void OnDisable() {
        if (EventManager.Instance != null) {
            EventManager.Instance.playerInputEvents.OnContinueAction -= ContinueStory;
        }
    }

    private void Start() {
        playerArrow.SetActive(false);
        GetChoicesText();
    }

    public void EnterDialogueMode(TextAsset inkJSON, GameObject npcArrow) {
        currentStory = new Story(inkJSON.text);
        currentStory.BindExternalFunction("PlayerCoins", () => StatsManager.Instance.GetPlayerCoins());

        speakerArrow = npcArrow;
        speakerArrow.SetActive(false);

        isDialoguePlaying = true;
        EventManager.Instance.dialogueEvents.DialogueStart();

        UIManager.Instance.ToggleDialogueUI(true);
        UIManager.Instance.ToggleDialogueChoiceUI(false);
        UIManager.Instance.ToggleStatsUI(false);
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.2f);

        if (playerArrow != null) {playerArrow.SetActive(false);}
        if (speakerArrow != null) {speakerArrow.SetActive(false);}

        EventManager.Instance.dialogueEvents.DialogueEnd();
        isDialoguePlaying = false;

        UIManager.Instance.ToggleDialogueUI(false);
        UIManager.Instance.ToggleDialogueChoiceUI(false);
        UIManager.Instance.ToggleStatsUI(true);
        dialogueText.text = "";

        speakerArrow = null;
        currentStory = null;
    }

    private void ContinueStory() {
        if (isDialoguePlaying && !isDisplayingChoices) {
            if (currentStory.canContinue) {
                dialogueText.text = currentStory.Continue();
                HandleTags(currentStory.currentTags);
                DisplayChoices();
            } else {
                StartCoroutine(ExitDialogueMode());
            }
        }
    }

    private void DisplayChoices() {
        List<Choice> currentChoices = currentStory.currentChoices;

        isDisplayingChoices = currentChoices.Count > 0;

        // This line can be used to create a choices entry animation
        if (!isDisplayingChoices) {
            if (choices[0].gameObject.activeSelf) {
                foreach (GameObject choice in choices) {
                    choice.SetActive(false);
                }
            }
            return;
        }

        if (currentChoices.Count > choices.Length) {
            Debug.LogError("More Choices than UI can support. Choices: " + currentChoices.Count);
        }

        int index = 0;
        UIManager.Instance.ToggleDialogueChoiceUI(true);
        foreach (Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choiceTexts[index].text = choice.text;
            index++;
        }
     }

     private void GetChoicesText() {
        choiceTexts = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choiceTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
     }

     public void MakeChoice(int choiceIndex) {
        isDisplayingChoices = false;
        EventSystem.current.SetSelectedGameObject(null);

        foreach (GameObject choice in choices) {
            choice.SetActive(false);
        }

        UIManager.Instance.ToggleDialogueChoiceUI(false);

        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
     }

     private void HandleTags(List<string> tags) {

        if (playerArrow != null && speakerArrow != null) {
            playerArrow.SetActive(false);
            speakerArrow.SetActive(false);
        }

        foreach (string tag in tags) {
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2) {
                Debug.LogWarning("Dialogue Manager: Tag format incorrect: " + tag);
                continue;
            }

            string key = splitTag[0].Trim().ToLower();
            string value = splitTag[1].Trim();

            switch (key) {
                case "speaker":
                    switch (value) {
                        case "player":
                            playerArrow.SetActive(true);
                            speakerArrow.SetActive(false);
                            break;
                        case "npc":
                            playerArrow.SetActive(false);
                            speakerArrow.SetActive(true);
                            break;
                    }
                    break;
                case "reward":
                    break;
            }
        }
     }

     public bool IsDialoguePlaying() => isDialoguePlaying;
}
