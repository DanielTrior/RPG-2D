using System.Runtime.InteropServices.WindowsRuntime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;
    private bool facingDirection = true;

    [Header("Jump")]
    [SerializeField] private LayerMask whatIsGround; // Layer mask for the ground
    [SerializeField] private float groundCheck; // Transform to check if the player is grounded
    [SerializeField][Range(1, 3)] private int numberOfJumps = 1; // Number of jumps the player can perform
    [SerializeField] private int currentNumberOfJumps; // Current number of jumps available
    private bool isGrounded; // Check if the player is grounded

    [Header("Dash")]
    [SerializeField] private float dashDuration; // Speed of the dash
    [SerializeField] private float dashTime; // Duration of the dash
    [SerializeField] private float dashSpeed; // Speed of the dash
    [SerializeField] private float dashCooldown; // Cooldown time for the dash
    [SerializeField] private float dashCooldownTime; // Time remaining for the dash cooldown

    [Header("Attack")]
    [SerializeField] private bool isAttacking; // Check if the player is attacking
    [SerializeField] private int comboCounter; // Counter for the attack combo
    [SerializeField] private float comboTime = .3f; // Time between attacks
    [SerializeField] private float comboTimeWindow; // Counter for the attack combo time


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CheckInput(); // Check for input (jump and movement)
        Movement(); // Call the movement method
        CollisionCheck(); // Check for collisions with the ground

        dashTime -= Time.deltaTime; // Decrease the dash time
        if(isGrounded) 
        {
            dashCooldownTime -= Time.deltaTime; // Decrease the dash cooldown
            if(!Input.GetButton("Jump")) // If the jump button is not pressed
                currentNumberOfJumps = numberOfJumps; // Reset the number of jumps available
        }
        comboTimeWindow -= Time.deltaTime; // Decrease the attack combo time counter

        FlipController(); // Call the flip controller method
        AnimatorController(); // Call the animator controller method
    }

    private void CollisionCheck()
    {
        // Check for collisions with the ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheck, whatIsGround);
    }

    public void AttackOver() // Method to handle player attack
    {
        isAttacking = false; // Set attacking state to false

        comboCounter++; // Increase the attack combo counter

        if(comboCounter > 2) // If the combo counter exceeds 2, reset it
            comboCounter = 0; // Reset the attack combo counter
    }

    private void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal"); // Get horizontal input

        if(Input.GetKeyDown(KeyCode.Mouse0)) // Check for attack input
        {
            StartAttackEvent();
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            currentNumberOfJumps--; // Decrease the number of jumps available
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)) // Check for dash input
            Dash();
    }

    private void StartAttackEvent()
    {
        if(!isGrounded) return; // If not grounded, do not attack
        
        if (comboTimeWindow < 0) // If the attack combo time counter is still active
            comboCounter = 0; // Reset the attack combo counter

        isAttacking = true; // Set attacking state to true
        comboTimeWindow = comboTime; // Reset the attack combo time counter
    }

    private void Dash()
    {
        if(dashCooldownTime < 0 && !isAttacking)
        {
            dashCooldownTime = dashCooldown; // Reset the dash cooldown time
            dashTime = dashDuration; // Reset the dash time
        }
    }

    private void Movement()
    {
        if(isAttacking) // If attacking, do not move
        {
            rb.velocity = new Vector2(0, 0); // Preserve vertical velocity while stopping horizontal movement
        }
        else if(dashTime > 0) // If dashing, do not move
        {
            rb.velocity = new Vector2(facingDirection ? dashSpeed : -dashSpeed, 0); // Apply dash speed based on facing direction while preserving vertical velocity
        }
        else // If not dashing, move normally
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (currentNumberOfJumps <= 0) return; // If not grounded, do not jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Flip() // Flip the player sprite based on movement direction
    {
        facingDirection = !facingDirection; // Toggle the facing direction
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); // Flip the player sprite;
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingDirection) // If moving right and facing left
            Flip(); // Flip the player sprite
        else if(rb.velocity.x < 0 && facingDirection) // If moving left and facing right
            Flip(); // Flip the player sprite
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0; // Check if the player is moving, temporaly variable
        // Set the animator parameters based on player state
        anim.SetFloat("yVelocity", rb.velocity.y); // Set the y velocity for jump animation
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0); // Set the dashing state
        anim.SetBool("isAttacking", isAttacking); // Set the attacking state
        anim.SetInteger("comboCounter", comboCounter); // Set the attack combo counter
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheck); // Draw a line to visualize the ground check
    }
}
