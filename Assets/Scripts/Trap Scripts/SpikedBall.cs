using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 pushDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(pushDirection,ForceMode2D.Impulse);
    }

}
