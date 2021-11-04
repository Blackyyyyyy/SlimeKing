using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float volume
    {
        get { return volumeStorage; }
        set
        {
            volumeStorage = value;
            foreach(AudioSource s in allAudioSources)
            {
                s.volume = value;
            }

            PlayerPrefs.SetFloat("Volume", value);
        }
    }
    private static float volumeStorage = 1;

    public static List<AudioSource> allAudioSources = new List<AudioSource>();

    #region Keyboard & Mouse

    public static KeyCode left = KeyCode.A;
    public static KeyCode right = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    
    public static KeyCode throwExplosive = KeyCode.Mouse0;

    public static KeyCode preciseScroll = KeyCode.LeftShift;
    
    public static KeyCode lookUp = KeyCode.Q;
    public static KeyCode accept = KeyCode.E;

    public static KeyCode pause = KeyCode.Escape;

    #endregion
}
