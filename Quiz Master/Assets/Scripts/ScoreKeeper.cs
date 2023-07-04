using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers;
    int questionsSeen;

   public int GetCorrectAnswers(){
        return correctAnswers;
   } 

   public void IncrementCorrectAnswers(){
        correctAnswers++;
   }

   public int GetQuestionsSeen(){
        return questionsSeen;
   } 

   public void IncrementQuestionsSeen(){
        questionsSeen++;
   }

   public float CalculateScore(){
    return Mathf.RoundToInt((float)correctAnswers/ (float)questionsSeen * 100);
   }
}
