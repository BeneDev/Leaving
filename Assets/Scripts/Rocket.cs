﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    //declaring variables
    private Rigidbody rb;
    private Vector3 velup;
    [SerializeField] float speed = 600f; 
    [SerializeField] float rotSpeed = 80; 
    private Vector3 rotLeft;
    private Vector3 rotRight;
    private AudioSource sfx;
	// Use this for initialization
	void Start () {
        //initializing variables
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
        //upwards veloctiy
        velup = new Vector3(0f, speed, 0f) * Time.deltaTime;
        //rotation values
        rotLeft = new Vector3(0f, 0f, rotSpeed) * Time.deltaTime;
        rotRight = new Vector3(0f, 0f, -rotSpeed) * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        Thrust();
	}

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            default:
                //player dies
                //Destroy(gameObject);
                break;

            case "Friendly":
                //do nothing
                break;
        }
    }

    private void Rotate()
    {
        rb.freezeRotation = true; // take full control over rotation
        if (Input.GetKey(KeyCode.A))
        {
            //rotate to the left here
            transform.Rotate(rotLeft);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotate to the right here
            transform.Rotate(rotRight);
        }
        rb.freezeRotation = false; // looses full control over rotation
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //makes the ship fly up
            rb.AddRelativeForce(velup);
            //plays the sound
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sfx.Play();
            }
        }
        //stops the sound
        else
        {
            sfx.Stop();
        }
    }
}
