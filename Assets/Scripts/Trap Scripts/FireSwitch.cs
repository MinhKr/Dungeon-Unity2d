using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    [SerializeField] TrapFire trapFire;
    Animator anim;

    public float timeNoActive;

    private void Start()
    {
        anim= GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null) 
        {
            anim.SetTrigger("pressed");
            trapFire.FireSwitchAfter(timeNoActive);           
        }
    }
}
