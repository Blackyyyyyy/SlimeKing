using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region Keyboard & Mouse

    public static KeyCode left = KeyCode.A;
    public static KeyCode right = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    
    public static KeyCode throwExplosive = KeyCode.Mouse0;

    public static KeyCode preciseScroll = KeyCode.LeftShift;
    
    public static KeyCode lookUp = KeyCode.Q;

    public static KeyCode pause = KeyCode.Escape;

    #endregion
    

    public static float fuseTime
    {
        get { return fuseTimeStorage; }
        set { fuseTimeStorage = Mathf.Round(Mathf.Clamp(value, minFuseTime, maxFuseTime) * 100f) / 100f; }
    }
    private static float fuseTimeStorage = 0.5f;

    public readonly static float minFuseTime = 0.5f;
    public readonly static float maxFuseTime = 4;
}
