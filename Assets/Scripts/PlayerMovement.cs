using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerMovement : MonoBehaviour
{
    public float maximumJumpHeight = 5.0f;
    public float timeToReachMaxHeight = 0.5f;
    public float fallMultiplier = 2.2f;
    public float lowJumpMultiplier = 2.0f;
    public float walkSpeed = 9.0f;
    public LayerMask platformMask;

    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private float gravity;
    private float jumpSpeed;

    private float xAxis;
    private float yAxis;
    private bool isGrounded;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        gravity = -2 * maximumJumpHeight / Mathf.Pow(timeToReachMaxHeight, 2);
        Physics2D.gravity = new Vector2(0, gravity);
        jumpSpeed = -gravity * timeToReachMaxHeight;
    }

    private void Update()
    {
        GetPlayerInputs();
        Walk(xAxis);
        Jump();
        GroundCheck();
            
    }

    private void GetPlayerInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody2D.velocity = Vector2.up * jumpSpeed;
        }

        if (rigidBody2D.velocity.y < 0)
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Walk(float move)
    {
        rigidBody2D.velocity = new Vector2(move * walkSpeed, rigidBody2D.velocity.y);
    }

    private void GroundCheck()
    {
        Bounds bounds = boxCollider2D.bounds;
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(bounds.min.x, bounds.min.y), Vector2.down, 0.1f, platformMask);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(bounds.max.x, bounds.min.y), Vector2.down, 0.1f, platformMask);

        isGrounded = hitLeft || hitRight;
    }
}
