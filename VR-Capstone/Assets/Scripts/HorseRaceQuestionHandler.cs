using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRaceQuestionHandler : MonoBehaviour
{
    public static bool isQuestioning;
    int answerSelected, correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        //isQuestioning = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void QuestionButtonOneSelected()
    {
        answerSelected = 1;
    }

    public void QuestionButtonTwoSelected()
    {
        answerSelected = 2;
    }

    public void QuestionButtonThreeSelected()
    {
        answerSelected = 3;
    }

    public void QuestionButtonFourSelected()
    {
        answerSelected = 4;
    }

}
