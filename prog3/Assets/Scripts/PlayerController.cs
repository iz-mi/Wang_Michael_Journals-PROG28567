using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rigbod;
    private FacingDirection facing = FacingDirection.right;

    public Transform footHitbox;
    public float footHitboxWidth = 0.1f;
    public LayerMask groundLayer;

    //create public variables for apex height and apex time
    public float jumpApexHeight = 2f;
    public float jumpApexTime = 0.1f;

    private float gravity;
    private float initialJumpVelocity;
    private float currentJumpVelocity;

    public float terminalSpeed = -10f;

    public float coyoteTime = 0.5f;
    private float coyoteTimeTimer;

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
        //rigbod.linearVelocity = new Vector2(playerInput.x * moveSpeed, rigbod.linearVelocity.y);

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
        float horizontalPlayerMovement = playerInput.x * moveSpeed;

        //jump controls
        //if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeTimer > 0f)
        {
            currentJumpVelocity = initialJumpVelocity;
            coyoteTimeTimer = 0f;
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

    public FacingDirection GetFacingDirection()
    {
        return facing;
    }
}
