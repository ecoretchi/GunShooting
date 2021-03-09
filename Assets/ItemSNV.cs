using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSNV : MonoBehaviour
{
    public Slider slider;
    public Text nameCaption;
    public Text value;
    public void SetNameCaption(string val)
    {
        nameCaption.text = val;
    }

    public void SetMaxValue(int val)
    {
        slider.maxValue = val;
    }

    public void SetValue(int val)
    {
        value.text = string.Format("{0}", val);
        slider.value = val;
    }
}
