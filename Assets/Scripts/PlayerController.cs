using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 3;
    private Vector2 moveInput;

    public Rigidbody2D rb;


    public Transform gunArm;

    private Camera theMainCam;

    public Animator anim;

    public GameObject bulletToFire;
    public Transform firePoint;

    
    public GameObject hitEffect;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f;
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    public float dashInvincibility = 0.5f;
    public float dashNoDmg = 0.5f;
    [HideInInspector]
    public float dashCounter, dashCoolCounter;

    public float timeBetweenShots = 0.001f;
    private float shotCounter;

    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        theMainCam = Camera.main;
        anim = GetComponent<Animator>();

        activeMoveSpeed = moveSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
        rb.velocity = moveInput * activeMoveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theMainCam.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        // Rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            /*
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            }
            */
            Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
            shotCounter = timeBetweenShots;
            AudioManager.instance.PlaySFX(12);

        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                anim.SetTrigger("dash");
                AudioManager.instance.PlaySFX(8);

                PlayerHealthController.instance.MakeInvicible(dashInvincibility);
            }
            

        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }


        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

    }

}
