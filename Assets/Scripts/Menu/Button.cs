using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private MenuManager menuManager;

    private void Start()
    {
        menuManager = MenuManager.current;
    }

    public void gotoMenu(int index)
    {
        menuManager.gotoMenu(index);
    }

    public void resetGame()
    {
        GameMemory.respawnPlayer();
        Timer.current.reset();
        GameData.save();
        unpauseGame();
    }

    public void resetScene()
    {
        resetGame();
        SceneManager.LoadScene("Level");
    }

    public void quitGame()
    {
        GameData.save();
        Application.Quit();
    }

    public void unpauseGame()
    {
        GameMemory.current.paused = false;
        menuManager.gotoMenu(0);
    }
}
