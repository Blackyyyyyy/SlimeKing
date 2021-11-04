using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyHitSounds : MonoBehaviour
{
    public AudioSource audioSource;

    private Rigidbody2D playerRigid;

    private void Start()
    {
        playerRigid = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.volume = playerRigid.velocity.magnitude * Settings.volume;
        audioSource.Play();
    }
}
