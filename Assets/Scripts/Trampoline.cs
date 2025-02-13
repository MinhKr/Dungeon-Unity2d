using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce;

    private Animator anim;

    [SerializeField] private bool canBeUse = true;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null && canBeUse)
        {
            canBeUse= false;
            anim.SetTrigger("activated");
            collision.GetComponent<PlayerMovement>().Push(pushForce);
        }
    }

    public void CanUse() => canBeUse = true;
}
