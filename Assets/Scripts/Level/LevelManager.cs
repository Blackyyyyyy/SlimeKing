using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;

    public static LevelManager current;

    public Transform player;

    public Level[] levels;
    public Level activeLevel
    {
        get { return activeLevelStorage; }
        set
        {
            if (activeLevel == value) return;
            if (activeLevel != null) activeLevel.gameObject.SetActive(false);
            activeLevelStorage = value;
            if (activeLevel != null) activeLevel.gameObject.SetActive(true);
            GameMemory.currentCamera = activeLevel.levelCamera;
        }
    }
    private Level activeLevelStorage;

    private bool gameEnded = false;

    void Start()
    {
        current = this;

        levels = transform.GetComponentsInChildren<Level>();

        foreach (Level level in levels) level.gameObject.SetActive(false);

        activeLevel = levels[0];
    }

    void FixedUpdate()
    {
        if (gameEnded) return;

        if (player.position.y > 162) endGame(); 
        activeLevel = levels[getCurrentLevelID()];
        currentLevel = getCurrentLevelID();
    }

    private int getCurrentLevelID()
    {
        return Mathf.Clamp((int)Mathf.Round(player.position.y / 12), 0, levels.Length - 1);
    }

    private void endGame()
    {
        gameEnded = true;
        Timer.current.stopped = true;
        GameMemory.getPlayer().GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<GameEnding>().transitionToEnd();
    }
}
