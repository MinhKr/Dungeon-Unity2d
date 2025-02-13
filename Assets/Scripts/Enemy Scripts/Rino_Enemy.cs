using UnityEngine;

public class Rino_Enemy : Enemy
{
    [SerializeField] private LayerMask whatIsPlayer;
    private RaycastHit2D playerDetection;

    [SerializeField]  private bool isAgressive;


    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        CollisionCheck();
        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 25, whatIsPlayer);

        if (playerDetection)
            isAgressive = true;
        else
            isAgressive = false;

        if (!isAgressive)
        {
            if (idleTimeCounter <= 0)
            {
                rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            idleTimeCounter -= Time.deltaTime;

            if (wallDetected)
            {
                idleTimeCounter = idleTime;
                Flip();
            }

        }
        else
        {
            if (idleTimeCounter <= 0)
                rb.velocity = new Vector2(8 * facingDirection, rb.velocity.y);
            else
                rb.velocity = Vector2.zero;

            idleTimeCounter -= Time.deltaTime;

            if (wallDetected)
            {
                isAgressive = false;
                idleTimeCounter = idleTime;
                Flip();
            }
        }


        anim.SetFloat("xVelocity", rb.velocity.x);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        /*Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + 25 * facingDirection, wallCheck.position.y));*/
    }
}
