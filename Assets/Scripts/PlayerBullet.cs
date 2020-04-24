﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int bulletDmg = 25;
    public float speed = 10f;
    public float bulletDieTimer = 0.05f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject enemyImpactEffect;
    bool hitTarget = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
        BulletDeathTimer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "shootable")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);

        }
        else if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            collision.GetComponentInParent<EnemyController>().DamageEnemy(bulletDmg);
        }

        if(collision.gameObject.tag == "EnemyBullet")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            BulletDeathTimer();
        }
 
    }

    private void BulletDeathTimer()
    {
        if (hitTarget)
        {
            bulletDieTimer -= 1f * Time.deltaTime;
            if(bulletDieTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
