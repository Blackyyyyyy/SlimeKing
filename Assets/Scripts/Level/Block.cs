using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public byte currentLevel = 0;
    public bool resetPosition = false;


    void Start()
    {
        string level = "Level" + currentLevel;
        transform.parent = GameObject.Find(level).transform;

        if (resetPosition) transform.localPosition = Vector3.zero;

        DestroyImmediate(this);
    }
}
