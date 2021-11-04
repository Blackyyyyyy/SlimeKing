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
        fuseTimeDisplay.minValue = TNT.minFuseTime;
        fuseTimeDisplay.maxValue = TNT.maxFuseTime;

        fuseTimeDisplay.value = TNT.fuseTime;
        valueDisplay.text = TNT.fuseTime.ToString();
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") == 0) return;
        TNT.fuseTime += Input.GetAxis("Mouse ScrollWheel") * 2
                          * (Input.GetKey(Settings.preciseScroll).ToByte() * -0.75f + 1);

        fuseTimeDisplay.value = TNT.fuseTime;
        valueDisplay.text = TNT.fuseTime.ToString();
    }
}
