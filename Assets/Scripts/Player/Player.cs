using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Attack")]
    public Vector2[] attackMovement;

    [Header("Move")]
    public float moveSpeed = 12;
    public float jumpForce = 12;
    public int facingDir {get; private set;} = 1;
    public bool facingRight = true;

    [Header("Dash")]
    public float dashSpeed; 
    public float dashDuration;
    public float dashDir {get; private set;}
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] public float dashUsageTimer;

    [Header("Jump")]
    public int maxJumps = 1;
    public int currentJumps = 0;

    
    public bool isBusy {get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDsitance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState {get; private set;}
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    #endregion

    private void Awake()
    {
        // Initialize the state machine and states
        stateMachine = new PlayerStateMachine();        
        idleState = new PlayerIdleState(stateMachine, this, "Idle");
        moveState = new PlayerMoveState(stateMachine, this, "Move");
        jumpState = new PlayerJumpState(stateMachine, this, "Jump");
        airState = new PlayerAirState(stateMachine, this, "Jump");
        dashState = new PlayerDashState(stateMachine, this, "Dash");   
        wallSlideState = new PlayerWallSlideState(stateMachine, this, "WallSlide"); 
        wallJumpState = new PlayerWallJumpState(stateMachine, this, "WallJump");
        primaryAttackState = new PlayerPrimaryAttackState(stateMachine, this, "Attack");
    }
 
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState); // Start with the idle state
    }

    private void Update()
    { 
        stateMachine.currentState.Update(); // Update the current state
        CheckForDashInput();
        if(isGroundDetected() && !Input.GetKey(KeyCode.Space)) // If the player is on the ground and not jumping
            currentJumps = maxJumps; // Reset the jump count when on the ground
    }

    public IEnumerator BusyFor(float _seconds)  // Coroutine to make the player busy for a certain amount of time
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();   // Trigger the animation finish event

    private void CheckForDashInput()
    {
        if(isWallDetected())
            return;
            
        if(isGroundDetected()) // If the player is on the ground, reset the dash cooldown timer
            dashUsageTimer -= Time.deltaTime * 2;

        dashUsageTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer <= 0) 
        {
            dashUsageTimer = dashCooldown; // Reset the cooldown timer

            dashDir = Input.GetAxis("Horizontal");

            if(dashDir == 0)
                dashDir = facingDir; // If no input, use the current facing direction

            stateMachine.ChangeState(dashState);    // Change to the dash state
        }
    }

    public void ResetDash() 
    {
        dashUsageTimer = 0; // Reset the cooldown timer
    }
    #region Velocity
    public void ZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0); // Stop movement
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision
    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDsitance, whatIsGround);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDsitance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void FlipController(float _x)
    {
        if(_x > 0 && !facingRight) // If moving right and facing left
            Flip(); // Flip the player sprite
        else if(_x < 0 && facingRight) // If moving left and facing right
            Flip(); // Flip the player sprite
    }
    #endregion
}
