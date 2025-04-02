using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatterGunTriggerHandler : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.value = 0f;
        slider.maxValue = 1f;
    }


    private void OnParticleTrigger()
    {
       if (HorseGameManager.hr_IsPlaying)
        {
            if (slider.value < 1f)
            {
                slider.value += 0.0025f;
            }
        }
        
    }
}
