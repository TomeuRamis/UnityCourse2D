using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    [Header("Answers")]
    [SerializeField] GameObject[] buttons = new GameObject[4];
    bool hasAnsweredEarly = false;
    [Header("Button Colors")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;
    [Header("Timer")]
    [SerializeField] Image timerImgae;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    [Header("Completition Slider")]
    [SerializeField] Slider slider;
    [Header("Global variables")]
    public bool complete = false;

    //[SerializeField] Color32 correctColor = new Color32(0,1,0,0);
    
    private void Awake() {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void Start()
    {
        //Init slider
        slider.minValue = 0;
        slider.maxValue = questions.Count;
        slider.value = 0;
    }

    void Update(){
        timerImgae.fillAmount = timer.fillFraction;
        if(timer.showNextQuestion){
            getNextQuestion();
            timer.showNextQuestion = false;
        }else if(!hasAnsweredEarly && !timer.isAnsweringQuestion){
            displayAnswer(-1);
            setButtonState(false);
        }
    }

    public void writeQuestion(){
        questionText.text = currentQuestion.getQuestion(); 
        for(int i = 0; i < buttons.Length; i++){
            TextMeshProUGUI answerText = buttons[i].GetComponentInChildren<TextMeshProUGUI>(); 
            answerText.text = currentQuestion.getAnswer(i);
        }  
    }

    void getNextQuestion(){
        if(questions.Count != 0){
            setButtonState(true);
            setButtonDefaultSprite();
            getRadnomQuestion();
            writeQuestion();
            scoreKeeper.IncrementQuestionsSeen();
            slider.value = scoreKeeper.GetQuestionsSeen();
            hasAnsweredEarly = false;
        }else{
            //Game Over
            complete = true;
        }    
    }

    private void getRadnomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        questions.Remove(currentQuestion);
    }

    public void onButtonSelected(int index){
        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
        scoreText.text = "Score: "+scoreKeeper.CalculateScore()+"%";
    }

    public void setButtonState(bool b){
        for(int i = 0; i < buttons.Length; i++){
            Button button = buttons[i].GetComponent<Button>(); 
            button.interactable = b;
        }
    }

    public void setButtonDefaultSprite(){
        for(int i = 0; i < buttons.Length; i++){
            Image imgButton = buttons[i].GetComponent<Image>(); 
            imgButton.sprite = defaultAnswerSprite;
        }
    }

    void displayAnswer(int index){
        Image buttonImg; 

        if(index == currentQuestion.getCorrectAnswerIndex()){
            Debug.Log("Correct!");
            questionText.text = "Correct!";
            buttonImg = buttons[index].GetComponent<Image>();
            buttonImg.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }else{
            Debug.Log("Incorrect!");
            questionText.text = "The correct answer was option "+(currentQuestion.getCorrectAnswerIndex()+1);
            buttonImg = buttons[currentQuestion.getCorrectAnswerIndex()].GetComponent<Image>();
            buttonImg.sprite = correctAnswerSprite;
        }
    }
}
