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
    public int bulletLife = 100;
    public int bulletLifeTick = 1;

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
    void FixedUpdate()
    {
        //rb.velocity = transform.right * speed;
        bulletLife -= bulletLifeTick;
        transform.position += direction * speed * Time.deltaTime;
        BulletDeathTimer(1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shootable")
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);

        }
        else if (collision.gameObject.tag == "Player")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            //Destroy(gameObject);
            PlayerHealthController.instance.DamagePlayer();
            AudioManager.instance.PlaySFX(4);
        }
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = false;
            //Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);

        }
        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
        }
        if (collision.gameObject.tag == "Breakable")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
        }
        if (collision.gameObject.tag == "Stage")
        {
            Instantiate(enemyImpactEffect, transform.position, transform.rotation);
            hitTarget = true;
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(4);
        }
    }

    private void BulletDeathTimer(float timeFactor)
    {
        if (hitTarget)
        {
            bulletDieTimer -= timeFactor * Time.deltaTime;
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
