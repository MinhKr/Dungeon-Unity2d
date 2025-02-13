using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom_enemy : Enemy
{
    [SerializeField] private GameObject[] wayPoints;
    private int currentWayPointIndex;
    protected override void Start()
    {
        base.Start();
    }


    private void Update()
    {

        /*rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);*/
        Move();

        FollowWayPoint();

        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    private void FollowWayPoint()
    {
        if (Vector2.Distance(wayPoints[currentWayPointIndex].transform.position, transform.position) < .1f)
        {
            Flip();
            currentWayPointIndex++;
            if (currentWayPointIndex >= wayPoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWayPointIndex].transform.position, speed * Time.deltaTime);
    }
}
