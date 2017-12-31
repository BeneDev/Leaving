using System;
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
    private Vector3 respawnPos;
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
                transform.position = respawnPos;
                transform.position += new Vector3(0f, 1.5f, 0f);
                transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                //Destroy(gameObject);
                break;

            case "Friendly":
                respawnPos = other.transform.position;
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
