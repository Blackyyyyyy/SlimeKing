using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MySystem
{
    public static byte ToByte(this bool boolean)
    {
        return Convert.ToByte(boolean);
    }

    public static int ToDirection(this bool boolean)
    {
        return Convert.ToByte(boolean) * 2 - 1;
    }
}
