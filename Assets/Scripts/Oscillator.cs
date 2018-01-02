using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movement = new Vector3(10f, 0, 0);
    [Range(0,1)] float movementFactor;
    [Range(0, 20)] [SerializeField] float period = 2f;

    private Vector3 startPos;
	void Start () {
        startPos = transform.position; // gets the starting position of the object
	}
	
	// Update is called once per frame
	void Update () {

        //prevent dividing by 0
        if(period <= Mathf.Epsilon) // Epsilon is the tiniest value a float can have so dont compare floats to zero (too inconsistent), but compare them to epsilon and never as "==" rather than "<="
        {
            return;
        }

        float cycles = Time.time / period; // determines how far into the sin wave we are

        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1

        movementFactor = rawSinWave / 2f + 0.5f; // goes from 0 to +1

        transform.position = startPos + (movement * movementFactor); // adds the current movement to the object's transform
	}
}
