using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerDisplay;
    public static Timer current;
    
    private float startTime;

    private float seconds;
    private int minutes;
    private int hours;

    public bool stopped = false;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        startTime = Time.time;
    }
    
    void Update()
    {
        if (stopped) return;

        seconds = Mathf.Round((Time.time - startTime) * 100) / 100f;

        if((int)seconds != 0 && (int)seconds % 60 == 0)
        {
            startTime = Time.time;
            minutes++;
            if(minutes % 60 == 0)
            {
                minutes = 0;
                hours++;
            }
        }

        timerDisplay.text = hours + ":" + minutes + ":" + seconds;
    }

    public void reset()
    {
        startTime = Time.time;
        minutes = 0;
        hours = 0;
    }

    public void setTimer(float seconds, int minutes, int hours)
    {
        startTime = -seconds;
        this.minutes = minutes;
        this.hours = hours;
    }

    public void saveTime()
    {
        PlayerPrefs.SetFloat("Seconds", seconds);
        PlayerPrefs.SetInt("Minutes", minutes);
        PlayerPrefs.SetInt("Hours", hours);
    }

    public string getTimeAsString()
    {
        return timerDisplay.text;
    }
}
