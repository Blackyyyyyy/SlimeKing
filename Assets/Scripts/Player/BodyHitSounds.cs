using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitSounds : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.volume = transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude * 0.02f;
        audioSource.Play();
    }
}
