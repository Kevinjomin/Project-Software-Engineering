using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_difficulty : MonoBehaviour
{
    private enum Difficulty
    {
        Easy, Medium, Hard
    }
    [SerializeField] private Difficulty difficultyLevel;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick(button));
    }

    private void OnButtonClick(Button button)
    {
        if(difficultyLevel == Difficulty.Easy)
        {
            gameManager.tempDifficulty = 1;
        }
        else if (difficultyLevel == Difficulty.Medium)
        {
            gameManager.tempDifficulty = 2;
        }
        else if (difficultyLevel == Difficulty.Hard)
        {
            gameManager.tempDifficulty = 3;
        }
        else
        {
            Debug.LogError("Difficulty level not set to button");
            return;
        }
        gameManager.StartNewRun();
    }
}
