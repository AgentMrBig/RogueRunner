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

    public GameObject playerToChase;

    public Animator anim;

    public GameObject[] deathSplatters;
    public GameObject hitEffect;

    public bool shouldShoot;

    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    public float fireCounter;

    public SpriteRenderer enemyBody;

    private Vector3 hitPosition;

    public bool playerSeen = false;

    public float shootRange = 10f;
    public float chaseRange = 15f;

    public float noDmgCount;
    public float noDmgLength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            CheckDecideShootMove();
        }

        if (noDmgCount > 0)
        {
            noDmgCount -= Time.deltaTime;
            if (noDmgCount <= 0)
            {
                enemyBody.color = new Color(1, 1f, 1f, 1f);
            }
        }

    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            if(collision.gameObject.GetComponent<EnemyBullet>().bulletLife < 75)
            {
                hitPosition = transform.position;
                
                DamageEnemy(50);
            }
        }
        
    }

    public void CheckDecideShootMove()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < chaseRange)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
            moveDirection.Normalize();

            MoveToPlayer(moveDirection);



            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }


        if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(bullet, transform.position, transform.rotation);
                AudioManager.instance.PlaySFX(13);
            }
        }
    }

    public void MoveToPlayer(Vector3 direction)
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * moveSpeed;
    }

    public void DamageEnemy(int dmg)
    {
        health -= dmg;
        noDmgCount = noDmgLength;
        enemyBody.color = new Color(1, 0.5f, 0.5f, 0.5f);

        AudioManager.instance.PlaySFX(2);
  

        rb.velocity = Vector3.zero;

        if (health <= 0)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(1);

            Instantiate(hitEffect, transform.position, transform.rotation);
            int selectedSplater = Random.Range(1, deathSplatters.Length);

            int rotation = Random.Range(0, 4);



            Instantiate(deathSplatters[selectedSplater], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));

        }
    }


}


    

    
