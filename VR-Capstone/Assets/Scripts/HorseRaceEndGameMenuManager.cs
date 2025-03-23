using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine.UI;



public class HorseRaceEndGameMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject endGameMenu;

    [SerializeField]
    Slider horsePlayer, horse2, horse3, horse4; //counts from the bottom up

    [SerializeField]
    TextMeshProUGUI firstPlaceTxt, secondPlacetxt, thirdPlacetxt, fourthPlacetxt;

    public class Racer
    {
        public string racerName;
        public float racerDistance;

        public Racer(string name, float value)
        {
            racerName = name;
            racerDistance = value;
        }
    }

    List<Racer> racers = new List<Racer>(4);

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

            Racer racer1 = new Racer("Player", horsePlayer.value);
            Racer racer2 = new Racer("Frank2", horse2.value);
            Racer racer3 = new Racer("Sally3", horse3.value);
            Racer racer4 = new Racer("Cherl4", horse4.value);

            racers.Add(racer1);
            racers.Add(racer2);
            racers.Add(racer3);
            racers.Add(racer4);

            racers = racers.OrderBy(racer => racer.racerDistance).ToList();

            for (int i = 0; i < 4; i++)
            {
                racers[i].racerDistance *= 100;
                racers[i].racerDistance = Mathf.Floor(racers[i].racerDistance);
            }

            //add in reverse order because greater the value the farther the horse traveled.
            firstPlaceTxt.text = racers[3].racerName + ": " + racers[3].racerDistance.ToString();
            secondPlacetxt.text = racers[2].racerName + ": " + racers[2].racerDistance.ToString();
            thirdPlacetxt.text = racers[1].racerName + ": " + racers[1].racerDistance.ToString();
            fourthPlacetxt.text = racers[0].racerName + ": " + racers[0].racerDistance.ToString();

            racers.Clear();
        }
        else
        {
            endGameMenu.SetActive(false);
        }
    }
}
