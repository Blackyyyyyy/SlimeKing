using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public SpriteRenderer whiteTransition;
    public Text endMessage;

    private bool transitioning = false;
    private float startTime;

    public void transitionToEnd()
    {
        transitioning = true;
        startTime = Time.time;
    }

    private void Update()
    {
        if (!transitioning) return;

        float alpha = (Time.time - startTime) * 0.2f;

        whiteTransition.color = new Color(0.855f, 0.855f, 0.855f, alpha);

        if (alpha >= 1)
        {
            transitioning = false;
            GameMemory.current.paused = true;
            MenuManager.current.gotoMenu(5);
            endMessage.text = "Thank you for playing my game!\nYour time:\n" + Timer.current.getTimeAsString() + "\n\nFeel free to tweet your best time at me!\n@Infinityyv on Twitter.";
        }
    }
}
