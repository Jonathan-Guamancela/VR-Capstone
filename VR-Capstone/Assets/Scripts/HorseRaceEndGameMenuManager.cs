using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HorseRaceEndGameMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject endGameMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        endGameMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(HorseGameManager.hr_InEndGameMenu)
        {
            endGameMenu.SetActive(true);
        }
        else
        {
            endGameMenu.SetActive(false);
        }
    }
}
