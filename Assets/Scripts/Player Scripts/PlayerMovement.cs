using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Move Infomation")]
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    private float dirX_Input;
    private bool canMove;
    private float defaultJumpForce;

    private bool canDoubleJump = true;


    [Header("Coyote Time")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Buffer Jump")]
    public float bufferJumpTime;
    public float bufferJumpTimer;

    [Header("Knock Back")]
    [SerializeField] private Vector2 knockbackDirection;
    private bool isKnocked;
    private bool canBeKnockback = true;
    [SerializeField] private float knockbackTime;
    [SerializeField] private float canBeKnockbackTime;


    [Header("Collision")]
    public LayerMask isGround;
    public LayerMask isWall;
    public float groundCheckDistance;
    public float wallCheckDistance;
    private bool isGrounded;
    private bool isWallDetected;

    [Header("Wall Slide and Wall Jump")]
    private bool canWallSlide;
    private bool isWallSliding;
    public Vector2 wallJumpDirection;

    [Header("Enemy")]
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;

    [Header("Appearing")]
    [SerializeField] private bool canBeControlled;

    [Header("DustFX")]
    [SerializeField] private ParticleSystem dustFx;

    private bool facingRight = true;
    private int facingDirection = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        SetAnimationForSkinPlayer();

        defaultJumpForce = jumpForce;
    }

    void Update()
    {
        AnimationController();

        if (isKnocked)
            return;

        FlipController();
        CollisionCheck();
        InputCheck();

        EnemyCheck();

        /*bufferJumpTimer -= Time.deltaTime;*/

        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;

            /*coyoteTimeCounter = coyoteTime;*/

            /*if(bufferJumpTimer > 0)
            {
                bufferJumpTimer = -1;
                Jump();
            }*/
        }
        /*else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }*/

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
        }

        Move();
    }

    private void EnemyCheck()
    {
        Collider2D[] hitedColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);

        foreach (var enemy in hitedColliders)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().Damage();
                anim.SetBool("rolling", true);
                Jump();
            }
        }
    }

    public void NoMoreRoll()
    {
        anim.SetBool("rolling", false);
    }

    private void InputCheck()
    {
        if (!canBeControlled) 
            return;

        dirX_Input = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Vertical") < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
        }
    }

    public void ReturnToControll()
    {
        canBeControlled = true;
    }

    private void JumpController()
    {
       /* if(!isGrounded)
        {
            bufferJumpTimer = bufferJumpTime;
        }*/

        if(isWallSliding)
        {
            canDoubleJump = true;
            WallJump();
        }
        else if (isGrounded /*|| coyoteTimeCounter > 0*/)
        {
            Jump();
        }
        else if(canDoubleJump)
        {
            canMove = true;
            canDoubleJump= false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce= defaultJumpForce;
        }
        canWallSlide = false;
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        /*coyoteTimeCounter = 0;*/
        AudioManager.instance.PlaySFX(1);
    }

    private void Move()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(moveSpeed * dirX_Input, rb.velocity.y);
        }
    }

    public void Push(float pushForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < -.1f)
        {
            Flip();
        }
        else if(!facingRight && rb.velocity.x > .1f)
        {
            Flip();
        }

    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector2(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }

    public void Knockback(Transform damageTransform)
    {
        if (canBeKnockback == false)
            return;
        isKnocked = true;

        canBeKnockback = false;

        #region Horizontal for Direction KnockBack
        int hDirection = 0;
        if (transform.position.x > damageTransform.position.x)
            hDirection = 1;
        else if (transform.position.x < damageTransform.position.x)
            hDirection = -1;
        #endregion

        rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);

        Invoke("CancelKnockback", knockbackTime);

        Invoke("AllowKnockBack", canBeKnockbackTime);
    }

    public void TakeDamage(float damage)
    {
        PlayerManager.instance.healthCurrent = Mathf.Clamp(PlayerManager.instance.healthCurrent - damage, 0, 3);
    }

     private void CancelKnockback()
     {
        isKnocked = false;
     } 

    private void AllowKnockBack()
    {
        canBeKnockback = true;
    }

    private void SetAnimationForSkinPlayer()
    {
        int skinChose = PlayerManager.instance.choosenSkinId; 
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }

        anim.SetLayerWeight(skinChose, 1);
    }

    private void AnimationController()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetBool("isGrounded", isGrounded);

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isWallSlide", isWallSliding);

        anim.SetBool("isWallDetected", isWallDetected);

        anim.SetBool("isKnocked", isKnocked);

        anim.SetBool("canbeControlled", canBeControlled);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, isGround);
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection , wallCheckDistance, isWall);

        if(isWallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }

        if (!isWallDetected)
        {
            isWallSliding = false;
            canWallSlide = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x , transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }
}
