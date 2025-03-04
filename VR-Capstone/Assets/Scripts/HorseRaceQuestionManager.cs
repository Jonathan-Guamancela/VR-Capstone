using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRaceQuestionManager : MonoBehaviour
{
    [SerializeField]
    GameObject questionsMenu;
    // Update is called once per frame
    void Update()
    {
        if (HorseGameManager.hr_IsQuestioning == true)
        {
            questionsMenu.SetActive(true);
        }
        else
        {
            questionsMenu.SetActive(false);
        }
    }
}
