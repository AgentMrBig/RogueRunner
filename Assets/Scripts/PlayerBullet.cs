using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "shootable")
        {
            Debug.Log("hit " + collision.gameObject.name);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
       
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
