using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Transform tr;

    [Header("Movement")]
    public float hSpeed = 5f;
    public float vSpeed = 10f;
    protected float direction = 0;

    [Header("Better Jumping")]
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 5f;

    [Header("Booleans")]
    public bool isOnFloor = false;
    public bool isJumping = false;
    public bool isFalling = false;
    public bool onRightWall = false;
    public bool onLeftWall = false;
    public bool facingRight = true;
    public bool jump = false;
  
    [Header("Collisions")]
    [SerializeField] private Vector2 bottomOffset = new Vector2(0, -0.5f);
    [SerializeField] private Vector2 rightOffset = new Vector2(0.25f, 0);
    [SerializeField] private Vector2 leftOffset = new Vector2(-0.25f, 0);
    [SerializeField] private float collisionRadius = 0.25f;
    public LayerMask groundLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isOnFloor)
            jump = true;

        CheckSlope();
        AirControl();
        BetterJump();
        CheckColisions();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hSpeed * direction, rb.velocity.y);
                
        if (jump)
        {
            rb.velocity = Vector2.up * vSpeed;
            jump = false;
        }

        if ((direction < 0 && facingRight) || (direction > 0 && !facingRight))
            Flip();
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void AirControl()
    {
        if (rb.velocity.y > 0f && !isOnFloor)
        {
            isJumping = true;
            isFalling = false;
        }
        else if (rb.velocity.y < 0f && !isOnFloor)
        {
            isJumping = false;
            isFalling = true;
        }
        else if (isOnFloor)
        {
            isJumping = false;
            isFalling = false;
        }
    }

    private void CheckColisions()
    {
        isOnFloor   = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset,  collisionRadius, groundLayer);
        onLeftWall  = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset,   collisionRadius, groundLayer);
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    public void CheckSlope()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3f, groundLayer);

        //Personagem esta no chao e parado e NÃO esta tentando pular 
        if (isOnFloor && direction == 0 && !Input.GetButton("Jump"))
        {
            // Verifica se está em uma inclinação
            if (hit && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                //Congelamos as constraints x e z do RigidBody2D 
                //Rb constraint is a bitmask use | to marge properties freeze rotation and freeze poition 
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            //Se está se movendo ou pulando descongelamos as constraints congelamos apenas a rotação Z
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            //Move Player up or down to compensate for the slope below them
            Vector3 newPosition = transform.position;
            newPosition.y += -hit.normal.x * Mathf.Abs(rb.velocity.x) * Time.fixedDeltaTime * (rb.velocity.x - hit.normal.x > 0 ? 1 : -1);
            transform.position = newPosition;
        }
    }
}
