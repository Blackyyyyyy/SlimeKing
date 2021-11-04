using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;

    private void Start()
    {
        slider.value = Settings.volume;
    }

    public void setVolume()
    {
        Settings.volume = slider.value;
        audioSource.volume = Settings.volume;
        audioSource.Play();
    }
}
