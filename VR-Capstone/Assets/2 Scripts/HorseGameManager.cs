using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HorseGameManager : MonoBehaviour
{

    [SerializeField]
    Slider horsePlayer, horse2, horse3, horse4;

    public static bool hr_IsPlaying, hr_IsQuestioning, hr_InEndGameMenu, hr_inMenu;

    private void Start()
    {
        //start in menue
        hr_inMenu = true;

        //turn off other menues
        hr_IsQuestioning = false;
        hr_IsPlaying = false;
        hr_InEndGameMenu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (horsePlayer.value == 1)
        {
            EndGame();
        }
        else if (horse2.value == 1)
        {
            EndGame();
        }
        else if (horse3.value == 1)
        {
            EndGame();
        }
        else if (horse4.value == 1)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        if (hr_IsPlaying)
        {
            //stop running the game
            hr_IsPlaying = false;

            //opens questions
            hr_IsQuestioning = true;
        }
    }

    public void PLayAgain()
    {
        //ressets horses
        horsePlayer.value = 0;
        horse2.value = 0;
        horse3.value = 0;
        horse4.value = 0;

        //hides menu and plays game
        hr_InEndGameMenu = false;
        hr_IsPlaying = true;
    }

    public void StartGame()
    {
        hr_inMenu = false;
        hr_IsPlaying = true;
    }
}