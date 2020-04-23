﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform gunArm;

    private Camera theMainCam;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        theMainCam = Camera.main;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
        rb.velocity = moveInput * moveSpeed;

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
