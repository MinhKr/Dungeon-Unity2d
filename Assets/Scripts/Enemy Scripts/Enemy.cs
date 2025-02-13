using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;

    protected int facingDirection = -1;

    [Header("Collision Check")]
    [SerializeField] protected LayerMask whatIsGround;
    /*[SerializeField] protected LayerMask whatToIgnore;*/
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance; 
    /*[SerializeField] protected float playerCheckDistance;*/
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform groundCheck;
    protected bool groundDetected;
    protected bool wallDetected;
    /*protected RaycastHit2D playerDetection; */

    [SerializeField] protected float idleTime = 2;
    [SerializeField] protected float idleTimeCounter;

    [SerializeField] protected float speed;

    protected bool canMove = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim= GetComponent<Animator>();
        rb= GetComponent<Rigidbody2D>();
        idleTimeCounter = idleTime;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Damage()
    {
        canMove = false;
        anim.SetTrigger("Hit");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        if(canMove) 
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
            player.TakeDamage(1);
            if(PlayerManager.instance.healthCurrent > 0)
            {
                player.Knockback(transform);
            }
            else
            {
                Debug.Log("End!");
            }
            
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionCheck() 
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
        /*playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, playerCheckDistance, ~whatToIgnore);*/
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
        /*Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + playerCheckDistance * facingDirection, wallCheck.position.y));*/
    }


}
