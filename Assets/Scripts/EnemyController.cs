using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 2f;

    public float rangeToChasePlayer = 20f;

    private Vector3 moveDirection;
    private bool moveDirectionSet = false;

    private GameObject playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer(moveDirection);
        /*
        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
        {
            moveDirection = PlayerController.instance.transform.position - transform.position;
        }
        moveDirection.Normalize();

        rb.velocity = moveDirection * moveSpeed;
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            playerToChase = collision.gameObject;
           // Debug.Log("hit " + collision.gameObject.name);
            moveDirection = playerToChase.transform.position - transform.position;
            moveDirection.Normalize();
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
}
