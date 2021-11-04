using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask groundLayer;
    public Rigidbody2D playerRigidBody;
    private SpriteRenderer[] spriteRenderers;

    public Animator animator;

    public AudioSource audioSource;

    public bool facing
    {
        get { return facingStorage; }
        set
        {
            if (facing == value) return;
            facingStorage = value;
            foreach(SpriteRenderer s in spriteRenderers)
            {
                s.flipX = value;
            }
        }
    }
    private bool facingStorage = false;

    private float speed = 1.7f;
    private float acceleration = 10;
    private float jumpForce = 5;
    private float friction = 0.1f;

    public bool grounded;

    private bool walking
    {
        get { return walkingStorage; }
        set
        {
            walkingStorage = value;
            animator.SetBool("walking", value);
        }
    }
    private bool walkingStorage;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        Settings.allAudioSources.Add(audioSource);
        audioSource.volume = Settings.volume;
    }

    private void FixedUpdate()
    {
        isGrounded();
        customFriction();

        if (grounded && MyInput.getAxisHorizontal() != 0)
        {
            walk();
        }
        else if (walking) walking = false;
    }
    
    void Update()
    {
        if (transform.position.y < -10) transform.position = new Vector3(0, -3.5f, 0);

        turn();
        jump();

        if (!grounded)
        {
            Vector2 velocity = playerRigidBody.velocity;
            if(Mathf.Abs(velocity.x) > 0.01f)
            {
                int moveDirection = (int)(velocity.x / Mathf.Abs(velocity.x));
                facing = Convert.ToBoolean(Mathf.Clamp01(moveDirection));
            }
            

            Transform animatorTransform = animator.transform;

            animatorTransform.LookAt((Vector2)transform.position + velocity);

            float angle = animatorTransform.rotation.eulerAngles.x;
            
            animatorTransform.rotation = Quaternion.Euler(0, 0, angle * -facing.ToDirection());
        }
    }

    private void turn()
    {
        float axisHorizontal = MyInput.getAxisHorizontal();
        if (axisHorizontal == 0) return;

        facing = (axisHorizontal > 0);
    }

    private void walk()
    {
        if(Mathf.Abs(playerRigidBody.velocity.x) < speed)
        {
            playerRigidBody.AddForce(new Vector2(acceleration * MyInput.getAxisHorizontal(), 0), ForceMode2D.Force);
        }

        if (!walking) walking = true;
    }

    private void jump()
    {
        if(grounded && MyInput.getInput_Jump())
        {
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        bool newGrounded = (Physics2D.OverlapBox(transform.position, new Vector2(0.424f, 0.01f), 0f, groundLayer) != null);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 0.02f);

        if(newGrounded != grounded)
        {
            animator.SetBool("grounded", newGrounded);

            if (newGrounded && !grounded)
            {
                float horizontalAxis = MyInput.getAxisHorizontal();

                playerRigidBody.velocity = Vector2.zero;
                //if (horizontalAxis == 0) playerRigidBody.velocity = Vector2.zero;
                //else if ((horizontalAxis < 0 && playerRigidBody.velocity.x > 0) || horizontalAxis > 0 && playerRigidBody.velocity.x < 0) playerRigidBody.velocity = Vector2.zero;

                animator.transform.rotation = transform.rotation;
                animator.transform.position = transform.position;

                audioSource.Play();
            }
            else //grounded == true ; newGrounded == false;
            {
                animator.transform.localPosition += Vector3.up * 0.5f;
            }
        }

        grounded = newGrounded;
        return grounded;
    }

    private void customFriction()
    {
        if(grounded && MyInput.getAxisHorizontal() == 0 && playerRigidBody.velocity != Vector2.zero)
        {
            Vector2 velocity = playerRigidBody.velocity;
            if ((velocity - velocity.normalized * friction).magnitude > velocity.magnitude) playerRigidBody.velocity = Vector2.zero;
            else playerRigidBody.velocity -= velocity.normalized * friction;
        }
    }
}
