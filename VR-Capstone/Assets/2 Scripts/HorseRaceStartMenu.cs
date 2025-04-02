using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRaceStartMenu : MonoBehaviour
{

    [SerializeField]
    GameObject startMenu;
    // Update is called once per frame
    void Update()
    {
        if (HorseGameManager.hr_inMenu == true)
        {
            startMenu.SetActive(true);
        }
        else
        {
            startMenu.SetActive(false);
        }
    }
}
