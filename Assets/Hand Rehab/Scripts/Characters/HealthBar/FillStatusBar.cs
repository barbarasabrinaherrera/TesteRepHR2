using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image fillImage;
    private Slider slider;

    public PlayerHealth PlayerHealth
    {
        get => default;
        set
        {
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
        if (slider.value > slider.maxValue && !fillImage.enabled)
        {
            fillImage.enabled = true;
        }
        float fillValue = playerHealth.currentHealth / playerHealth.maxHealth;
        if (fillValue <= slider.maxValue/5) //Se menor que 20% de vida
        {
            fillImage.color = Color.red;
        }
        else if (fillValue > slider.maxValue/5) //Se maior que 20%
        {
            fillImage.color = Color.green;
        }
        slider.value = fillValue;
    }
}
