using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {
    
    [Header("Game Over UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI TimeTxt;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// Called when the script is enabled. This function updates the score and time text in the Game Over UI.
    /// </summary>
    private void OnEnable() {
        scoreText.text = $"x{StatsManager.Instance.GetScore()}";

        int gameTime = GameManager.Instance.GetTimer();
        int minutes = gameTime / 60;
        int seconds = gameTime % 60;

        TimeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        animator.Play("GameOverUI");
    }
}
