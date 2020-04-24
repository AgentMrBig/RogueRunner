using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletDmg = 25;
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public GameObject enemyImpactEffect;
    private Vector3 direction;

    bool hitTarget = false;
    public float bulletDieTimer = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = transform.right * speed;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shootable")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            BulletDeathTimer();
            //Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            BulletDeathTimer();
            //Destroy(gameObject);
            collision.GetComponentInParent<PlayerController>().DamagePlayer(bulletDmg);
        }
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            BulletDeathTimer();
        }

    }

    public void BulletDeathTimer()
    {
        if (hitTarget)
        {
            bulletDieTimer -= 1f * Time.deltaTime;
            if (bulletDieTimer <= 0)
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
