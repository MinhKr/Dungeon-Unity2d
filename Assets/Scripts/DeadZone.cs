using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Game Over!");
            collision.transform.position = PlayerManager.instance.posStart;
            //Destroy(collision.gameObject);
        }
    }
}
