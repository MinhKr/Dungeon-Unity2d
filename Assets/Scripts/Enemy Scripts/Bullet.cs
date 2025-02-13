using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Trap
{
    private int ground;
    private float xSpeed;
    private float ySpeed;
    [SerializeField] private Rigidbody2D rb;
    private void Awake()
    {
        ground = LayerMask.NameToLayer("Ground");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == ground)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        rb.velocity = new Vector2(xSpeed , ySpeed);
    }

    public void SetUpSpeed(float x , float y)
    {
        xSpeed= x;
        ySpeed= y;  
    }


}
