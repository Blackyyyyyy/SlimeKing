using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static void save()
    {
        Transform player = GameMemory.getPlayer();

        PlayerPrefs.SetFloat("PositionX", player.position.x);
        PlayerPrefs.SetFloat("PositionY", player.position.y);

        PlayerPrefs.SetFloat("VelocityX", player.GetComponent<Rigidbody2D>().velocity.x);
        PlayerPrefs.SetFloat("VelocityY", player.GetComponent<Rigidbody2D>().velocity.y);
    }

    public static void load()
    {
        GameMemory.getPlayer().position = new Vector2(PlayerPrefs.GetFloat("PositionX"), PlayerPrefs.GetFloat("PositionY"));
        GameMemory.getPlayer().GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerPrefs.GetFloat("VelocityX"), PlayerPrefs.GetFloat("VelocityY"));
    }
}
