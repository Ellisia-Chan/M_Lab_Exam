using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguDataSO", menuName = "ScriptableObjects/DialoguDataSO")]
public class DialoguDataSO : ScriptableObject {
    public List<string> dialogueLines;

    [Header("Interaction Requirements")]
    public int requiredCoins;

    [Header("Quiz")]
    public bool HasQuiz;
    public string quizQuestion;
    public string[] quizAnswers = new string[4];
    public int correctAnswerIndex;

    [Header("Reward")]
    public int diamondReward;
}
