using System.Collections;
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
    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
        BulletDeathTimer(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "shootable")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);



        }
        else if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);

            collision.GetComponentInParent<EnemyController>().DamageEnemy(bulletDmg);
        }

        if(collision.gameObject.tag == "EnemyBullet")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);


        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);


        }

        if (collision.gameObject.tag == "Breakable")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);


        }

        if (collision.gameObject.tag == "Stage")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);


        }

    }

    private void BulletDeathTimer(float timeFactor)
    {
        if (hitTarget)
        {
            bulletDieTimer -= timeFactor * Time.deltaTime;
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
