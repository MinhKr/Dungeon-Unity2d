using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TrapFire : Trap
{
    public bool isBurning;
    private Animator anim;
    public float repeatingRate;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if(transform.parent == null)
        {
            InvokeRepeating("FireSwitch", 0, repeatingRate);
        }
    }

    private void Update()
    {
        anim.SetBool("isBurning" , isBurning);   
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(isBurning)
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    public void FireSwitchAfter(float seconds)
    {
        CancelInvoke();
        isBurning = false;
        Invoke("FireSwitch", seconds);
    }

    public void FireSwitch()
    {
        isBurning = !isBurning;
    }
}
