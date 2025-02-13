using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : Enemy
{
    private bool playerDetected;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool canShoot;

    [Header("Plant Bullet")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();

        playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 20, whatIsPlayer);
        if(playerDetected)
            canShoot = true;
        else
            canShoot = false;

        if (idleTimeCounter < 0 && canShoot)
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
        }
        idleTimeCounter -= Time.deltaTime;
    }

    public void Shooting()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

        newBullet.GetComponent<Bullet>().SetUpSpeed(speed * facingDirection, 0);
    }
}
