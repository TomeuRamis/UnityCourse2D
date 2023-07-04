using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToAnswer = 30f;
    [SerializeField] float timeToShowAnswer = 10f;
    public bool isAnsweringQuestion = false;
    public float fillFraction = 1;
    public bool showNextQuestion;
    float timerValue;

    void Update()
    {
        updateTimer();
    }

    void updateTimer(){
        timerValue -= Time.deltaTime;
        //Actualizamos fillFraction 
        if(timerValue > 0 && isAnsweringQuestion){
            fillFraction = timerValue/timeToAnswer;
        }else if(timerValue > 0 && !isAnsweringQuestion){
            fillFraction = timerValue/timeToShowAnswer;
        }
        //Termina el tiempo de responder
        else if(timerValue <= 0 && isAnsweringQuestion){
            isAnsweringQuestion = false;
            timerValue = timeToShowAnswer;
        }
        //Termina tiempo de ver respuesta
        else if(timerValue <= 0 && !isAnsweringQuestion){
            isAnsweringQuestion = true;
            timerValue = timeToAnswer;     
            showNextQuestion = true;      
        }
        
        //Debug.Log("Timer value:"+timerValue+" FillFraction="+fillFraction);
    }

    public void cancelTimer(){
        timerValue = 0;
    }
}
