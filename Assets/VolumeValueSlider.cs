using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValueSlider : MonoBehaviour
{
    public TextMeshProUGUI volumeValue;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        SetVolumeValue(slider.value);
    }

    public void SetVolumeValue(float value)
    {
        volumeValue.text = value.ToString();
    }
}
