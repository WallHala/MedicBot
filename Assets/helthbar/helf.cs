using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class helf : MonoBehaviour
{
    public Slider sliders;
    public Gradient gradient;
    public Image fill;
    public void setmaxhealth(float helth)
    {
        sliders.maxValue = helth;
        sliders.value = helth;
        fill.color=gradient.Evaluate(1f);
    }
    public void sethelth(float helth)
    {
        sliders.value = helth;
        fill.color = gradient.Evaluate(sliders.normalizedValue);
    }
}
