using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyInput
{
    public static float getAxisHorizontal()
    {
        return Mathf.Clamp(-Convert.ToInt16(Input.GetKey(Settings.left)) +
                                      Convert.ToInt16(Input.GetKey(Settings.right)) +
                                      Input.GetAxis("HorizontalC"), -1, 1);
    }

    public static bool getInput_Jump()
    {
        return Input.GetKeyDown(Settings.jump);
    }

    public static bool getInputDown_ThrowTNT()
    {
        return Input.GetKeyDown(Settings.throwExplosive);
    }

    public static bool getInputUp_ThrowTNT()
    {
        return Input.GetKeyUp(Settings.throwExplosive);
    }
    
}
