using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float lifetime = 15;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (startTime + lifetime <= Time.time) Destroy(gameObject);
    }
}
