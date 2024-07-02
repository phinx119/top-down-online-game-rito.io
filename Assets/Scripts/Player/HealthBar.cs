using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthBar(float currentValue, float maxValue)
    {
        healthSlider.value = currentValue / maxValue;
    }
}
