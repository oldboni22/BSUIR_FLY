using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private void Start()
    {
        _slider.value = PlayerPrefsManager.Volume;
    }
    public void OnSliderChanged(Single value)
    {
        PlayerPrefsManager.Volume = value;
    }
}
