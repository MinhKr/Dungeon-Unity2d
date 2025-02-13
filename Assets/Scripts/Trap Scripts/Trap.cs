using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();

            player.TakeDamage(1);
            if (PlayerManager.instance.healthCurrent > 0)
            {
                player.Knockback(transform);
            }
            else
            {
                Debug.Log("End!");
            }
        }
    }
}
