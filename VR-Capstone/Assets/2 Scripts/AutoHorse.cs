using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoHorse : MonoBehaviour
{
    public Slider slider;
    int horseDecision = 0;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (HorseGameManager.hr_IsPlaying)
        {
            horseDecision = Random.Range(0, 100) % 5;
            if (horseDecision == 0)
            {
                slider.value += Random.Range(0, 0.002f);
            }
        }
    }
}
