
using UnityEngine;

public class Player_State_Manager : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float gravityScale = 3f;

    [Header("Detection")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float groundCheckRadius = 0.1f;
    public float wallCheckDistance = 0.5f;
    public Transform groundCheck;

    private Collider2D col;

    private bool isClimbing = false;
    private bool isGrounded = false;
    private bool isTouchingWall = false;
    private Rigidbody2D rb;
    private Vector2 velocity;
    private float horizontalInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer | wallLayer);

        isTouchingWall = false;
        if (horizontalInput != 0)
        {
            Vector2 dir = horizontalInput < 0 ? Vector2.left : Vector2.right;
            Bounds bounds = col.bounds;
            Vector2 boxSize = new Vector2(0.05f, bounds.size.y * 1f); // thin box, almost full height
            Vector2 origin = bounds.center;

            RaycastHit2D hit = Physics2D.BoxCast(origin, boxSize, 0f, dir, wallCheckDistance, wallLayer);

            if (!hit)
            {
                hit = Physics2D.BoxCast(origin, boxSize, 0f, dir, wallCheckDistance, groundLayer);
            }

            isTouchingWall = hit && (hit.collider.CompareTag("Block") || ((1 << hit.collider.gameObject.layer) & wallLayer) != 0);
        }

        isClimbing = isTouchingWall && horizontalInput != 0;

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isClimbing)
        {
            jumpRequested = true;
        }

        DebugLog();
    }

    void FixedUpdate()
    {
        velocity = rb.linearVelocity;

        if (isClimbing)
        {
            velocity.y = Input.GetAxisRaw("Vertical") * (moveSpeed / 2);
            velocity.x = 0;
        }
        else
        {
            velocity.x = horizontalInput * moveSpeed;

            if (jumpRequested)
            {
                velocity.y = jumpForce;
                jumpRequested = false;
            }

            velocity.y += Physics2D.gravity.y * gravityScale * Time.fixedDeltaTime;
        }

        rb.linearVelocity = velocity;
    }

    void DebugLog()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log($"[Jump Attempt] isGrounded={isGrounded} | isClimbing={isClimbing} | groundCheck.pos={groundCheck.position} | radius={groundCheckRadius} | layerMask={groundLayer.value}");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (col != null)
        {
            Bounds bounds = col.bounds;
            Vector2 boxSize = new Vector2(0.05f, bounds.size.y * 0.9f);

            Gizmos.color = isTouchingWall ? Color.green : Color.yellow;

            // Left check
            Vector3 leftCenter = bounds.center + Vector3.left * wallCheckDistance;
            Gizmos.DrawWireCube(leftCenter, boxSize);

            // Right check
            Vector3 rightCenter = bounds.center + Vector3.right * wallCheckDistance;
            Gizmos.DrawWireCube(rightCenter, boxSize);
        }
    }
}