using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rigbod;
    private FacingDirection facing = FacingDirection.right;

    //checkGround var
    public Transform footHitbox;
    public float footHitboxWidth = 0.1f;
    public LayerMask groundLayer;

    //jump & gravity var
    public float jumpApexHeight = 2f;
    public float jumpApexTime = 0.1f;
    private float gravity;
    private float initialJumpVelocity;
    private float currentJumpVelocity;
    public float terminalSpeed = -10f;
    
    //coyote time var
    public float coyoteTime = 0.5f;
    private float coyoteTimeTimer;

    //dash mechanics
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;

    //check isDash for movableBox interactions
    private bool isDashing = false;
    private float dashRemaining = 0f;
    private float dashCooldownTimer = 0f;
    private float dashDirection = 1f;

    //wall jump var
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public float wallCheckHitbox = 0.1f;
    public float wallJumpSpeed = 5f;
    private float wallJumpLaunch = 0f;

    //double jump var
    public int doubleJumps = 1;
    private int doubleJumpsRemaining;

    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        
        //disable gravity value on rigibody
        rigbod.gravityScale = 0f;

        gravity = (-2f * jumpApexHeight) / (jumpApexTime * jumpApexTime);
        initialJumpVelocity = (2f * jumpApexHeight) / jumpApexTime;
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 playerInput = new Vector2(moveX, 0f);
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        //wall jump
        bool LWallCheck = false;
        bool RWallCheck = false;

        bool onWallCheck = !IsGrounded() && (LWallCheck || RWallCheck);

        //update player facing direction every frame otherwise the movable box gimmick doesn't work
        if (playerInput.x > 0.1f)
        {
            facing = FacingDirection.right;
        }
        else if (playerInput.x < -0.1f)
        {
            facing = FacingDirection.left;
        }

        if (leftWallCheck != null)
        {
            LWallCheck = Physics2D.OverlapCircle(leftWallCheck.position, wallCheckHitbox, groundLayer);
        }

        if (rightWallCheck != null)
        {
            RWallCheck = Physics2D.OverlapCircle(rightWallCheck.position, wallCheckHitbox, groundLayer);
        }

        //tim allen zoom
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (isDashing)
        {
            dashRemaining -= Time.deltaTime;
            if (dashRemaining <= 0f)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
            }
        }

        //dash keybind
        //if (!isDashing && dashCooldownTimer <= 0f && IsGrounded() && Input.GetKeyDown(KeyCode.LeftShift))
        if (!isDashing && dashCooldownTimer <= 0f && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            dashRemaining = dashDuration;

            if (playerInput.x > 0.1f)
            {
                dashDirection = 1f;
            }

            else if (playerInput.x < -0.1f)
            {
                dashDirection = -1f;
            }
        }

        //coyote time timer calc
        if (IsGrounded())
        {
            coyoteTimeTimer = coyoteTime;
        }

        else
        {
            coyoteTimeTimer -= Time.deltaTime;
        }
        
        //horizontal movement
        float horizontalPlayerMovement;

        if (isDashing)
        {
            horizontalPlayerMovement = dashDirection * dashSpeed;
        }
        else if (wallJumpLaunch != 0f)
        {
            horizontalPlayerMovement = wallJumpLaunch;
        }
        else
        {
            horizontalPlayerMovement = playerInput.x * moveSpeed;
        }

        //OLD JUMP MECHANICS (no wall check)
        /*if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeTimer > 0f && !isDashing)
        {
            currentJumpVelocity = initialJumpVelocity;
            coyoteTimeTimer = 0f;
        }*/

        //NEW JUMP MECHANICS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onWallCheck && !isDashing)
            {
                currentJumpVelocity = initialJumpVelocity;

                float wallDirection;

                if (LWallCheck)
                {
                    wallDirection = 1f;
                }
                else
                {
                    wallDirection = -1f;
                }

                wallJumpLaunch = wallDirection * wallJumpSpeed;

                coyoteTimeTimer = 0f;
            }

            //normal jump
            else if (coyoteTimeTimer > 0f && !isDashing && IsGrounded())
            {
                currentJumpVelocity = initialJumpVelocity;
                coyoteTimeTimer = 0f;
            }
        }

        currentJumpVelocity += gravity * Time.deltaTime;

        //terminal velocity
        if (currentJumpVelocity < terminalSpeed)
        {
            currentJumpVelocity = terminalSpeed;
        }

        //check for grounded 
        if (IsGrounded() && currentJumpVelocity < 0.01f)
        {
            currentJumpVelocity = 0f;
        }

        rigbod.linearVelocity = new Vector2(horizontalPlayerMovement, currentJumpVelocity);
        

        wallJumpLaunch = 0f;
    }

    public bool IsWalking()
    {
        if (rigbod.linearVelocity.x > 0.1f)
        {
            facing = FacingDirection.right;
        }

        else if (rigbod.linearVelocity.x < -0.1f)
        {
            facing = FacingDirection.left;
        }

        return true;
    }

    public bool IsGrounded()
    {
        Collider2D collision = Physics2D.OverlapCircle(footHitbox.position, footHitboxWidth, groundLayer);

        if (collision != null)
        {
            Debug.Log("return true");
            return true;
        }
        else
        {
            Debug.Log("return false");
            return false;
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public float GetDashDirection()
    {
        return dashDirection;
    }

    public FacingDirection GetFacingDirection()
    {
        return facing;
    }
}
