using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour
{
    public static GameMemory current;
    public static Camera currentCamera;

    public Transform player;
    private static readonly Vector2 playerSpawn = new Vector2(0, -5);

    public int activeLevelID = 0;

    public bool paused
    {
        get { return pausedStorage; }
        set
        {
            Time.timeScale = (!value).ToByte();
            pausedStorage = value;

            if (value) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Confined;
        }
    }
    private bool pausedStorage = false;

    private float saveInterval = 10f;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Confined;

        current = this;

        firstBootCheck();
        StartCoroutine("invokeLoad");

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

    public static void respawnPlayer()
    {
        current.player.position = playerSpawn;
    }

    private void invokeSave()
    {
        GameData.save();
    }

    private IEnumerator invokeLoad()
    {
        yield return new WaitForEndOfFrame();
        GameData.load();
    }

    private void firstBootCheck()
    {
        if (!PlayerPrefs.HasKey("PositionX")) PlayerPrefs.SetFloat("PositionX", 0);
        if (!PlayerPrefs.HasKey("PositionY")) PlayerPrefs.SetFloat("PositionY", -4);

        if (!PlayerPrefs.HasKey("VelocityX")) PlayerPrefs.SetFloat("VelocityX", 0);
        if (!PlayerPrefs.HasKey("VelocityY")) PlayerPrefs.SetFloat("VelocityY", 0);

        if (!PlayerPrefs.HasKey("Volume")) PlayerPrefs.SetFloat("Volume", Settings.volume);

        if (!PlayerPrefs.HasKey("Seconds")) PlayerPrefs.SetFloat("Seconds", 0);
        if (!PlayerPrefs.HasKey("Minutes")) PlayerPrefs.SetInt("Minutes", 0);
        if (!PlayerPrefs.HasKey("Hours")) PlayerPrefs.SetInt("Hours", 0);

        if (!PlayerPrefs.HasKey("Best Time")) PlayerPrefs.SetString("Best Time", "0:0:00.00");
    }
}
