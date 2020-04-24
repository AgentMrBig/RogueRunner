using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public int health = 100;

    private Vector3 moveDirection;
    private bool moveDirectionSet = false;

    private GameObject playerToChase;

    public Animator anim;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float fireCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MoveToPlayer(moveDirection);
        /*
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;
        */

        if(moveDirection != Vector3.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (shouldShoot)
        {
            fireCounter -= Time.deltaTime;
            if(fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            playerToChase = collision.gameObject;
            Debug.Log("hit " + collision.gameObject.name);
            moveDirection = playerToChase.transform.position - transform.position;
            moveDirection.Normalize();
            Debug.Log(moveDirection);
            moveDirectionSet = true;
            rb.velocity = moveDirection * moveSpeed;
        }

    }

    public void MoveToPlayer(Vector3 direction)
    {
        if (moveDirectionSet)
        {
            direction = playerToChase.transform.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * moveSpeed;
        }
        
    }

    public void DamageEnemy(int dmg)
    {
        health -= dmg;
        rb.velocity = Vector3.zero;

        if(health <= 0)
        {
            Destroy(gameObject);

            Instantiate(hitEffect, transform.position, transform.rotation);
            int selectedSplater = Random.Range(1, deathSplatters.Length);
            
            int rotation = Random.Range(0, 4);

            

            Instantiate(deathSplatters[selectedSplater], transform.position, Quaternion.Euler(0f,0f,rotation * 90f));
            
        }
    }
}
