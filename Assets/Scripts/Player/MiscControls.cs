using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscControls : MonoBehaviour
{
    public MenuManager menuManager;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.current;
    }

    void Update()
    {
        if (Input.GetKeyDown(Settings.lookUp))
        {
            GameMemory.current.paused = true;
            int newLevelID = Mathf.Clamp(levelManager.activeLevel.levelID + 1, 0, levelManager.levels.Length - 1);
            levelManager.activeLevel = levelManager.levels[newLevelID];
        }
        else if (Input.GetKeyUp(Settings.lookUp))
        {
            GameMemory.current.paused = false;
            int newLevelID = Mathf.Clamp(levelManager.activeLevel.levelID - 1, 0, levelManager.levels.Length - 1);
            levelManager.activeLevel = levelManager.levels[newLevelID];
        }

        if(Input.GetKeyDown(Settings.pause))
        {
            GameMemory.current.paused = !GameMemory.current.paused;
            menuManager.gotoMenu(GameMemory.current.paused.ToByte());
        }
    }
}
