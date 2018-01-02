using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    //declaring variables
    private Rigidbody rb;
    private Vector3 velup;
    [SerializeField] float speed = 600f; 
    [SerializeField] float rotSpeed = 80;
    [SerializeField] float levelLoadDelay;
    private Vector3 rotLeft;
    private Vector3 rotRight;
    private AudioSource sfx;
    private Vector3 respawnPos;
    private int level;
    enum State {Alive, Dying, Transcending}
    State state;
    [SerializeField] AudioClip thrust;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem smoke;
	void Start ()
    {
        //initializing variables
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
        //upwards veloctiy
        velup = new Vector3(0f, speed, 0f) * Time.deltaTime;
        //rotation values
        rotLeft = new Vector3(0f, 0f, rotSpeed) * Time.deltaTime;
        rotRight = new Vector3(0f, 0f, -rotSpeed) * Time.deltaTime;
        level = 0;
        state = State.Alive;
	}
	
	void Update ()
    {
        if (state == State.Alive || state == State.Transcending)
        {
            Rotate();
            Thrust();
        }
        else
        {
            if (smoke.isPlaying)
            {
                smoke.Stop();
            }
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if(state != State.Alive) { return; } //ignore collisions when dead or transcending
        switch (other.gameObject.tag)
        {
            default:
                //player respawns
                sfx.Stop();
                sfx.PlayOneShot(death);
                state = State.Dying;
                Invoke("Respawn", 0.8f);
                break;

            case "Friendly":
                //Sets the Point as Respawn position
                respawnPos = other.transform.position;
                break;

            case "Finish":
                //adds one to the level count, prevents the player from moving and loads next level
                level++;
                state = State.Transcending;
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
        }
    }

    private void Respawn()
    {
        transform.position = respawnPos;
        transform.position += new Vector3(0f, 1.5f, 0f);
        transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        state = State.Alive;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(level);
        state = State.Alive;
    }

    private void Rotate()
    {
        rb.angularVelocity = Vector3.zero; // take full control over rotation
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
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!smoke.isPlaying)
            {
                smoke.Play();
            }
            //makes the ship fly up
            rb.AddRelativeForce(velup);
            //plays the sound
            if (!sfx.isPlaying)
            {
                sfx.PlayOneShot(thrust);
            }
        }
        else
        {
            sfx.Stop();
            if(smoke.isPlaying)
            {
                smoke.Stop();
            }
        }
    }
}
