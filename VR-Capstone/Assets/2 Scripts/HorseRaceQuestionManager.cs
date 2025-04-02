using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRaceQuestionManager : MonoBehaviour
{
    [SerializeField]
    GameObject questionsMenu;

    bool questioning = false;
    // Update is called once per frame
    void Update()
    {
        if (HorseGameManager.hr_IsQuestioning == true)
        {
            if (!questioning)
            {
                GetComponent<QuizManager>().RetryGame();
                questionsMenu.SetActive(true);
                questioning = true;
            }
        }
        else
        {
            questionsMenu.SetActive(false);
            questioning = false;
        }
    }
}
