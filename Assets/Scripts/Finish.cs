using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject message;

    public byte type;

    private void teleportToFinish()
    {
        GameMemory.getPlayer().position = new Vector2(0, 103);
        message.SetActive(true);
    }

    private void retryGame()
    {
        GameMemory.getPlayer().position = new Vector2(0, -4);
        message.SetActive(false);
        GameData.save();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch(type)
            {
                case 0:
                    teleportToFinish();
                    break;
                case 1:
                    retryGame();
                    break;
                default:
                    retryGame();
                    break;
            }
        }
    }
}
