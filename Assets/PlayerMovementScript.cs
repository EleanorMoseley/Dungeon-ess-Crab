using System;
using UnityEngine;
using System.Collections.Generic;

public enum PlayerState { Normal, Jumping, Climbing }

public class Player_State_Manager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float acceleration = 50f; // how fast you reach max speed
    public float groundDeceleration = 40f; // friction
    public float gravityScale = 3f; // custom gravity strength

    private PlayerState currentState = PlayerState.Normal;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private float horizontalInput;
    private bool jumpRequested;

    public WallSensor WallColliderL;
    public WallSensor WallColliderR;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WallColliderL = transform.GetChild(0).GetComponent<WallSensor>();
        WallColliderR = transform.GetChild(1).GetComponent<WallSensor>();
    }

    void Update()
    {
        // 1. Get Input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Enable/Disable Sensors based on direction
        // We disable both if there is no horizontal input
        WallColliderL.gameObject.SetActive(horizontalInput < 0);
        WallColliderR.gameObject.SetActive(horizontalInput > 0);

        // 3. Check if the ACTIVE sensor is touching a wall
        // Since only one can be active at a time, we check both; 
        // the inactive one will always return false.
        bool isPushingIntoWall = WallColliderL.IsTouchingWall || WallColliderR.IsTouchingWall;

        if (WallColliderL.IsTouchingWall && horizontalInput < 0)
        {
            currentState = PlayerState.Climbing;
        }
        else if (WallColliderR.IsTouchingWall && horizontalInput > 0)
        {
            currentState = PlayerState.Climbing;
        }
        else if (currentState == PlayerState.Climbing && !isPushingIntoWall)
        {
            // If we stop pushing or leave the wall, we fall
            currentState = PlayerState.Jumping;
        }

        // 4. Handle Jumping from Normal state
        if (Input.GetKeyDown(KeyCode.W) && currentState == PlayerState.Normal)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // Read current velocity from physics engine
        velocity = rb.linearVelocity;

        switch (currentState)
        {
            case PlayerState.Normal:
                ApplyMovement();
                ApplyGravity();
                break;
            case PlayerState.Jumping:
                ApplyMovement();
                ApplyGravity();
                break;
            case PlayerState.Climbing:
                ClimbUpdatePhysics();
                break;
        }

        // 2. Apply calculated velocity back to Rigidbody
        rb.linearVelocity = velocity;
    }

    void ApplyMovement()
    {
        if (horizontalInput != 0)
        {
            // Accelerate towards move speed
            velocity.x = Mathf.MoveTowards(velocity.x, horizontalInput * moveSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Apply friction/slow down
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.fixedDeltaTime);
        }

        if (jumpRequested)
        {
            velocity.y = jumpForce;
            currentState = PlayerState.Jumping;
            jumpRequested = false;
        }
    }

    void ApplyGravity()
    {
        // Simple manual gravity
        velocity.y += Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;
    }

    void ClimbUpdatePhysics()
    {
        // Zero out gravity while climbing
        velocity.y = Input.GetAxisRaw("Vertical") * (moveSpeed / 2);
        velocity.x = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            currentState = PlayerState.Normal;
        }
        //else if (collision.gameObject.CompareTag("Wall"))
        //{
        //    currentState = PlayerState.Climbing;
        //}
    }
}