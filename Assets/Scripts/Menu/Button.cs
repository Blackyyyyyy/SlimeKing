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
        menuManager.playButtonSound();
        menuManager.gotoMenu(index);
    }

    public void resetGame()
    {
        menuManager.playButtonSound();
        GameMemory.respawnPlayer();
        Timer.current.reset();
        GameData.save();
        unpauseGame();
    }

    public void resetScene()
    {
        menuManager.playButtonSound();
        resetGame();
        Settings.allAudioSources = new List<AudioSource>();
        SceneManager.LoadScene("Level");
    }

    public void quitGame(bool save)
    {
        menuManager.playButtonSound();
        if(save) GameData.save();
        Application.Quit();
    }

    public void unpauseGame()
    {
        menuManager.playButtonSound();
        GameMemory.current.paused = false;
        menuManager.gotoMenu(0);
    }

    public void gotoScene(string scene)
    {
        menuManager.playButtonSound();
        Settings.allAudioSources = new List<AudioSource>();
        SceneManager.LoadScene(scene);
    }
}
