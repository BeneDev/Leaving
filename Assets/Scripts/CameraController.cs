using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Rocket player;
    private float offX;
    private float offY;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Rocket>();
        offX = transform.position.x - player.transform.position.x;
        offY = transform.position.y - player.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x + offX, player.transform.position.y + offY, transform.position.z);
    }
}
