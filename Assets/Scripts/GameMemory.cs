using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour
{
    public static GameMemory current;
    public static Camera currentCamera;

    public Transform player;

    public int activeLevelID = 0;

    public bool paused
    {
        get { return pausedStorage; }
        set
        {
            Time.timeScale = (!value).ToByte();
            pausedStorage = value;
        }
    }
    private bool pausedStorage = false;

    private float saveInterval = 10f;

    private void Start()
    {
        current = this;
        if (!PlayerPrefs.HasKey("PositionX")) PlayerPrefs.SetFloat("PositionX", 0);
        if (!PlayerPrefs.HasKey("PositionY")) PlayerPrefs.SetFloat("PositionY", -4);

        if (!PlayerPrefs.HasKey("VelocityX")) PlayerPrefs.SetFloat("VelocityX", 0);
        if (!PlayerPrefs.HasKey("VelocityY")) PlayerPrefs.SetFloat("VelocityY", 0);

        GameData.load();

        InvokeRepeating("invokeSave", saveInterval, saveInterval);
    }

    private void Update()
    {
        activeLevelID = LevelManager.current.activeLevel.levelID;
    }

    public static Transform getPlayer()
    {
        return current.player;
    }

    private void invokeSave()
    {
        GameData.save();
    }
}
