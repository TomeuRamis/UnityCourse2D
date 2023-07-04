using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName ="Quiz Question", fileName ="New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4]; 
    [SerializeField] int correctAnswer = 0;

    public string getQuestion(){
        return question;
    }

    public string getAnswer(int i){
        return answers[i];
    }

    public int getCorrectAnswerIndex(){
        return correctAnswer;
    }
}
