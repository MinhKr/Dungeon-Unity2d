using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint;
    private void Awake()
    {
        PlayerManager.instance.respawnPoint = resPoint;
        PlayerManager.instance.PlayerRespawn();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null) {
            GetComponent<Animator>().SetTrigger("touch");
        }
    }
}
