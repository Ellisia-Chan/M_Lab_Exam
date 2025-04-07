using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance { get; private set; }

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject continueIcon;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] private TextMeshProUGUI[] choiceTexts;

    [Header("Player Arrow")]
    [SerializeField] private GameObject playerArrow;

    private Story currentStory;
    private bool isDialoguePlaying = false;
    private bool isDisplayingChoices = false;

    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = false;

    private GameObject speakerArrow;
    private string currentNPCID;

    private Dictionary<string, bool> interactedNPCS = new Dictionary<string, bool>();

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
        EventBus.Subscribe<PlayerInputEventContinue>(e => ContinueStory());
    }

    private void OnDisable() {
        EventBus.UnSubscribe<PlayerInputEventContinue>(e => ContinueStory());

    }

    private void Start() {
        playerArrow.SetActive(false);
        GetChoicesText();
    }


    /// <summary>
    /// Enters dialogue mode with the specified ink JSON, NPC arrow, and NPC ID.
    /// Initializes the current story, binds external functions for interaction tracking,
    /// and updates the UI accordingly.
    /// </summary>
    /// <param name="inkJSON">The ink JSON asset containing the dialogue script.</param>
    /// <param name="npcArrow">The game object representing the NPC's arrow.</param>
    /// <param name="npcID">The unique identifier of the NPC.</param>
    public void EnterDialogueMode(TextAsset inkJSON, GameObject npcArrow, string npcID) {
        currentStory = new Story(inkJSON.text);
        currentNPCID = npcID;


        if (currentNPCID != null) {
            currentStory.BindExternalFunction("HasInteracted", () => interactedNPCS.ContainsKey(currentNPCID) && interactedNPCS[currentNPCID]);
            currentStory.BindExternalFunction("SetHasInteracted", () => interactedNPCS[currentNPCID] = true);

            if (currentNPCID.Contains("FrogQuiz") && !interactedNPCS.ContainsKey(currentNPCID)) {
                EventBus.Publish(new FrogEventInteracted());
            }
        }

        speakerArrow = npcArrow;
        if (speakerArrow != null) { speakerArrow.SetActive(false); }

        isDialoguePlaying = true;
        canContinueToNextLine = true;

        EventBus.Publish(new DialogueStartEvent());

        UIManager.Instance.ToggleDialogueUI(true);
        UIManager.Instance.ToggleDialogueChoiceUI(false);
        UIManager.Instance.ToggleStatsUI(false);
        ContinueStory();
    }


    /// <summary>
    /// Exits the dialogue mode by disabling the player and speaker arrows, 
    /// notifying the event manager of the dialogue end, and resetting the UI.
    /// </summary>
    /// <returns>An IEnumerator to handle the asynchronous operation.</returns>
    private IEnumerator ExitDialogueMode() {
        yield return new WaitForSeconds(0.5f);

        if (playerArrow != null) { playerArrow.SetActive(false); }
        if (speakerArrow != null) { speakerArrow.SetActive(false); }

        EventBus.Publish(new DialogueEndEvent());
        isDialoguePlaying = false;

        UIManager.Instance.ToggleDialogueUI(false);
        UIManager.Instance.ToggleDialogueChoiceUI(false);
        UIManager.Instance.ToggleStatsUI(true);
        dialogueText.text = "";

        speakerArrow = null;
        currentStory = null;

        EventSystem.current.SetSelectedGameObject(null);
    }


    /// <summary>
    /// Continues the story by displaying the next line of dialogue or presenting choices to the player.
    /// </summary>
    private void ContinueStory() {
        if (canContinueToNextLine) {
            if (isDialoguePlaying && !isDisplayingChoices) {
                if (currentStory.canContinue) {

                    if (displayLineCoroutine != null) {
                        StopCoroutine(displayLineCoroutine);
                    }

                    displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

                    HandleTags(currentStory.currentTags);
                } else {
                    StartCoroutine(ExitDialogueMode());
                }
            }
        }
    }


    /// <summary>
    /// Displays a line of dialogue, character by character, with a typing effect.
    /// 
    /// This coroutine takes a string of text and displays it on the dialogue UI, 
    /// adding each character with a delay to simulate typing. It also handles 
    /// rich text formatting by checking for '<' and '>' characters.
    /// 
    /// Parameters:
    ///     line (string): The line of dialogue to display.
    /// 
    /// Returns:
    ///     IEnumerator: A coroutine that displays the line of dialogue.
    /// </summary>
    private IEnumerator DisplayLine(string line) {
        dialogueText.text = "";
        canContinueToNextLine = false;
        continueIcon.SetActive(false);
        HideChoices();

        bool isAddingRichText = false;

        foreach (char letter in line.ToCharArray()) {

            if (letter == '<' || isAddingRichText) {
                isAddingRichText = true;
                dialogueText.text += letter;
                
                if (letter == '>') {
                    isAddingRichText = false;
                }

            } else {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        canContinueToNextLine = true;
        continueIcon.SetActive(true);
        DisplayChoices();
    }


    /// <summary>
    /// Displays the available choices to the player based on the current story state.
    /// 
    /// This function checks if there are any choices available in the current story and 
    /// updates the UI accordingly. If there are choices, it enables the corresponding 
    /// UI elements and sets their text. If there are no choices, it disables the UI elements.
    /// 
    /// Parameters: None
    /// 
    /// Returns: None
    /// </summary>
    private void DisplayChoices() {
        if (currentStory == null) {
            Debug.LogError("currentStory is null! Cannot display choices.");
            return;
        }

        if (currentStory.currentChoices == null) {
            Debug.LogError("currentChoices is null! There are no choices available.");
            return;
        }

        List<Choice> currentChoices = currentStory.currentChoices;

        isDisplayingChoices = currentChoices.Count > 0;

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


    /// <summary>
    /// Hides all the choice buttons by setting their active state to false.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void HideChoices() {
        foreach (GameObject choiceButton in choices) {
            choiceButton.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Retrieves the TextMeshProUGUI components from the choice game objects.
    /// 
    /// Parameters: None
    /// Returns: None
    /// </summary>
    private void GetChoicesText() {
        choiceTexts = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices) {
            choiceTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }


    /// <summary>
    /// Makes a choice in the current dialogue by selecting the option at the specified index.
    /// 
    /// This function checks if the dialogue can continue to the next line, then updates the UI 
    /// accordingly by hiding the choices and toggling the dialogue choice UI. It then selects 
    /// the choice at the specified index in the current story and continues the story.
    /// 
    /// Parameters:
    ///     choiceIndex (int): The index of the choice to select.
    /// 
    /// Returns:
    ///     None
    /// </summary>
    public void MakeChoice(int choiceIndex) {
        if (canContinueToNextLine) {
            isDisplayingChoices = false;
            EventSystem.current.SetSelectedGameObject(null);

            foreach (GameObject choice in choices) {
                choice.SetActive(false);
            }

            UIManager.Instance.ToggleDialogueChoiceUI(false);

            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
    }


    /// <summary>
    /// Handles the tags in the dialogue system.
    /// 
    /// This function iterates through the list of tags, splits each tag into a key-value pair,
    /// and performs the corresponding action based on the key.
    /// </summary>
    /// <param name="tags">The list of tags to handle.</param>
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

            Debug.Log($"{key} {value}");
            switch (key) {
                case "speaker":
                    switch (value) {
                        case "player":
                            if (playerArrow != null) {
                                playerArrow.SetActive(true);
                            }

                            if (speakerArrow != null) {
                                speakerArrow.SetActive(false); 
                            }

                            break;
                        case "npc":
                            if (playerArrow != null) {
                                playerArrow.SetActive(false); 
                            }

                            if (speakerArrow != null) {
                                speakerArrow.SetActive(true); 
                            }
                            break;
                    }
                    break;
                case "reward":
                    Debug.Log($"reward: {value}");
                    //EventManager.Instance.coinEvents.CoinCollected(int.Parse(value));
                    EventBus.Publish(new CoinCollectedEvent(int.Parse(value)));
                    break;
                case "coinspend":
                    Debug.Log($"coinSpend: {value}");
                    //EventManager.Instance.coinEvents.CoinSpend(int.Parse(value));
                    EventBus.Publish(new CoinSpendEvent(int.Parse(value)));
                    break;
            }
        }
    }


    /// <summary>
    /// Returns a boolean indicating whether the dialogue is currently playing.
    /// </summary>
    /// <returns>True if the dialogue is playing, false otherwise.</returns>
    public bool IsDialoguePlaying() => isDialoguePlaying;
}
