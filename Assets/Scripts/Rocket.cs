using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    //declaring variables
    private Rigidbody rb;
    private Vector3 velup;
    private Vector3 rotLeft;
    private Vector3 rotRight;
	// Use this for initialization
	void Start () {
        //initializing variables
        rb = GetComponent<Rigidbody>();
        //upwards veloctiy
        velup = new Vector3(0, 600, 0) * Time.deltaTime;
        //rotation values
        rotLeft = new Vector3(0, 0, 50) * Time.deltaTime;
        rotRight = new Vector3(0, 0, -50) * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        Processnput();
	}

    private void Processnput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //makes the ship fly up
            rb.AddRelativeForce(velup);
        }
        if(Input.GetKey(KeyCode.A))
        {
            //rotate to the left here
            transform.Rotate(rotLeft);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //rotate to the right here
            transform.Rotate(rotRight);
        }
    }
}
