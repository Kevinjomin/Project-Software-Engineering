using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a class to hold all the predetermined questions (non-random)
public class QuestionsList : MonoBehaviour
{
    public List<Question> Questions = new List<Question>();
}

[System.Serializable]
public class Question
{
    public string questionText;

    public int answer1;
    public int answer2;
    public int answer3;
    public int answer4;

    public int correctAnswer;
}
