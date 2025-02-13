using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : Trap
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    private float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position , transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length) {
                currentWaypointIndex= 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
