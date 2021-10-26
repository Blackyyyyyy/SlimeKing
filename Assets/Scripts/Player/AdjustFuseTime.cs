using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustFuseTime : MonoBehaviour
{
    public Slider fuseTimeDisplay;
    public Text valueDisplay;

    private void Start()
    {
        fuseTimeDisplay.minValue = Settings.minFuseTime;
        fuseTimeDisplay.maxValue = Settings.maxFuseTime;

        fuseTimeDisplay.value = Settings.fuseTime;
        valueDisplay.text = Settings.fuseTime.ToString();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        Settings.fuseTime += Input.GetAxis("Mouse ScrollWheel") * 2
                          * (Input.GetKey(Settings.preciseScroll).ToByte() * -0.75f + 1);

        fuseTimeDisplay.value = Settings.fuseTime;
        valueDisplay.text = Settings.fuseTime.ToString();
    }
}
