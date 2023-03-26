using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    static public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate { ChangeVolume(); } ); 
    }

    static public void ChangeVolume()
    {
        ControlPanelScript.audioSource.volume = slider.value;
    }
}
