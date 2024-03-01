using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    private enum QuestionType
    {
        Addition, Substraction, Multiplication
    }
    private QuestionType questionType;

    public TMP_Text questionText;
    public Button choice1;
    public Button choice2;
    public Button choice3;
    public Button choice4;

    public int firstNumber;
    public int secondNumber;
    public int correctAnswer;

    public int wrongAnswer1;
    public int wrongAnswer2;
    public int wrongAnswer3;

    public void StartQuestionManager()
    {

        //pick question type randomly
        int enumCount = System.Enum.GetValues(typeof(QuestionType)).Length;
        questionType = (QuestionType)Random.Range(0, enumCount);

        if(questionType == QuestionType.Addition)
        {
            setAdditionQuestion();
        }
        else if (questionType == QuestionType.Substraction)
        {
            setSubstractionQuestion();
        }
        else if (questionType == QuestionType.Multiplication)
        {
            setMultiplicationQuestion();
        }
        AssignAnswersToButton();

        choice1.enabled = true;
        choice2.enabled = true;
        choice3.enabled = true;
        choice4.enabled = true;

        choice1.onClick.AddListener(() => OnButtonClick(choice1));
        choice2.onClick.AddListener(() => OnButtonClick(choice2));
        choice3.onClick.AddListener(() => OnButtonClick(choice3));
        choice4.onClick.AddListener(() => OnButtonClick(choice4));
    }

    void setAdditionQuestion()
    {
        firstNumber = Random.Range(10, 100);
        secondNumber = Random.Range(10, 100);
        correctAnswer = firstNumber + secondNumber;

        questionText.GetComponentInChildren<TMP_Text>().text = firstNumber + " + " + secondNumber;

        do {
            wrongAnswer1 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer1 == correctAnswer);

        do {
            wrongAnswer2 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1);

        do {
            wrongAnswer3 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer3 == correctAnswer || wrongAnswer3 == wrongAnswer1 || wrongAnswer3 == wrongAnswer2);
    }
    void setSubstractionQuestion()
    {
        firstNumber = Random.Range(10, 100);
        secondNumber = Random.Range(1, firstNumber - 1);
        correctAnswer = firstNumber - secondNumber;

        questionText.GetComponentInChildren<TMP_Text>().text = firstNumber + " - " + secondNumber;

        do {
            wrongAnswer1 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer1 == correctAnswer);

        do {
            wrongAnswer2 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1);

        do {
            wrongAnswer3 = correctAnswer + Random.Range(-9, 9);
        } while (wrongAnswer3 == correctAnswer || wrongAnswer3 == wrongAnswer1 || wrongAnswer3 == wrongAnswer2);
    }
    void setMultiplicationQuestion()
    {
        firstNumber = Random.Range(1, 10);
        secondNumber = Random.Range(2, 10);
        correctAnswer = firstNumber * secondNumber;

        questionText.GetComponentInChildren<TMP_Text>().text = firstNumber + " x " + secondNumber;

        do {
            wrongAnswer1 = correctAnswer + firstNumber * Random.Range(-2, 3);
        } while (wrongAnswer1 == correctAnswer);

        do {
            wrongAnswer2 = correctAnswer + firstNumber * Random.Range(-2, 3);
        } while (wrongAnswer2 == correctAnswer || wrongAnswer2 == wrongAnswer1);

        do {
            wrongAnswer3 = correctAnswer + firstNumber * Random.Range(-2, 3);
        } while (wrongAnswer3 == correctAnswer || wrongAnswer3 == wrongAnswer1 || wrongAnswer3 == wrongAnswer2);
    }

    void AssignAnswersToButton()
    {
        RemoveButtonListener();

        int[] order = { 1, 2, 3, 4 };
        Shuffle(order);

        choice1.GetComponentInChildren<TMP_Text>().text = GetAnswerByOrder(order[0]);
        choice2.GetComponentInChildren<TMP_Text>().text = GetAnswerByOrder(order[1]);
        choice3.GetComponentInChildren<TMP_Text>().text = GetAnswerByOrder(order[2]);
        choice4.GetComponentInChildren<TMP_Text>().text = GetAnswerByOrder(order[3]);
    }

    string GetAnswerByOrder(int order)
    {
        if (order == 1)
        {
            return correctAnswer.ToString();
        }
        else if (order == 2)
        {
            return wrongAnswer1.ToString();
        }
        else if (order == 3)
        {
            return wrongAnswer2.ToString();
        }
        else if (order == 4)
        {
            return wrongAnswer3.ToString();
        }
        else
        {
            return "Invalid"; // Handle invalid order
        }
    }

    void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    void RemoveButtonListener()
    {
        choice1.onClick.RemoveAllListeners();
        choice2.onClick.RemoveAllListeners();
        choice3.onClick.RemoveAllListeners();
        choice4.onClick.RemoveAllListeners();
    }

    private IEnumerator checkAnswer(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        // Check if the clicked button's text matches the correct answer
        if (button.GetComponentInChildren<TMP_Text>().text == correctAnswer.ToString())
        {
            Debug.Log("Correct Answer!");
            buttonImage.color = new Color(0, 255, 0);
        }
        else
        {
            Debug.Log("Wrong Answer!");
            buttonImage.color = new Color(255, 0, 0);
        }
        choice1.enabled = false;
        choice2.enabled = false;
        choice3.enabled = false;
        choice4.enabled = false;

        yield return new WaitForSeconds(0.5f);

        buttonImage.color = new Color(255, 255, 255);

        // Start a new question
        StartQuestionManager();
    }

    private void OnButtonClick(Button button)
    {
        StartCoroutine(checkAnswer(button));
    }
}
